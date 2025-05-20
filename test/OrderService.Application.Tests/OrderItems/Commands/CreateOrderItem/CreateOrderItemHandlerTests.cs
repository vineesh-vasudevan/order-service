using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.OrderItems.Commands.CreateOrderItem;
using OrderService.Contracts.Dto.Input;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Mocks.Domain;

namespace OrderService.Application.Tests.OrderItems.Commands.CreateOrderItem
{
    [TestFixture]
    public class CreateOrderItemHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private CreateOrderItemHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new CreateOrderItemHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Handle_ShouldAddOrderItem_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();

            var order = new OrderBuilder()
                .WithId(orderId)
                .Build();

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.From(order));

            var request = new CreateOrderItemRequestDto
            {
                Id = orderItemId,
                ProductCode = "ABC123",
                Quantity = 2,
                UnitPrice = 10m,
                TotalPrice = 20m
            };

            var command = new CreateOrderItemCommand(request, orderId);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(orderItemId);

            _orderRepository.Received(1).Update(Arg.Is<Order>(o =>
                o.Items.Any(i => i.Id.Value == orderItemId)));

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
            foreach (var item in order.Items)
            {
                item.Should().NotBeNull();
                item.OrderId.Value.Should().Be(orderId);
                item.Id.Value.Should().Be(orderItemId);
                item.ProductCode.Should().Be(request.ProductCode);
                item.Quantity.Should().Be(request.Quantity);
                item.UnitPrice.Should().Be(request.UnitPrice);
                item.TotalPrice.Should().Be(request.TotalPrice);
            }
        }

        [Test]
        public void Handle_ShouldThrow_WhenOrderNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.None);

            var CreateOrderItemRequest = new CreateOrderItemRequestDto
            {
                Id = orderItemId,
                ProductCode = "ABC123",
                Quantity = 2,
                UnitPrice = 10m,
                TotalPrice = 20m
            };

            var command = new CreateOrderItemCommand(CreateOrderItemRequest, orderId);

            // Act & Assert
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<OrderNotFoundException>();
            _orderRepository.DidNotReceive().Update(Arg.Any<Order>());
            _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }
    }
}