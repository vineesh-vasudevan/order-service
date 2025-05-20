using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.Orders.Commands.CancelOrder;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Mocks.Domain;

namespace OrderService.Application.Tests.Orders.Commands.CancelOrder
{
    [TestFixture]
    public class CancelOrderHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private CancelOrderHandler _handler;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CancelOrderHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Handle_ShouldCancelOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = OrderMocks.MockOrder(orderId, orderItemId);

            _orderRepository
                .GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(order);

            var command = new CancelOrderCommand(orderId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _orderRepository.Received(1).Update(Arg.Is<Order>(o => o.Status == OrderStatus.Cancelled));
            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());

            order.Status.Should().Be(OrderStatus.Cancelled);
            foreach (var orderItem in order.Items)
            {
                orderItem.Status.Should().Be(OrderItemStatus.Cancelled);
            }
        }

        [Test]
        public void Handle_ShouldThrow_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.None);

            var command = new CancelOrderCommand(orderId);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<OrderNotFoundException>();

            _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }
    }
}