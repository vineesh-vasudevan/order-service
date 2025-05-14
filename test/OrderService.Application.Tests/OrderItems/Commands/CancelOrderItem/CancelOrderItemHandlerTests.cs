using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.OrderItems.Commands.CancelOrderItem;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Mocks;

namespace OrderService.Application.Tests.OrderItems.Commands.CancelOrderItem
{
    [TestFixture]
    public class CancelOrderItemHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private CancelOrderItemHandler _sut;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new CancelOrderItemHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Should_Cancel_OrderItem_And_Commit_Transaction_When_Order_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = MockOrder(orderId, orderItemId);

            var command = new CancelOrderItemCommand(orderId, orderItemId);
           
            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.From(order));

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _orderRepository.Received(1).Update(order);
            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
            foreach (var item in order.Items)
            {
                item.Status.Should().Be(OrderItemStatus.Cancelled);
            }           
        }

        [Test]
        public async Task Should_Throw_OrderNotFoundException_And_Rollback_When_Order_Not_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var command = new CancelOrderItemCommand(orderId, Guid.NewGuid());

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.None);

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<OrderNotFoundException>();
            _orderRepository.DidNotReceive().Update(Arg.Any<Order>());
            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Should_Throw_OrderItemNotFoundException_And_Rollback_When_Order_Not_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = MockOrder(orderId, orderItemId);

            var command = new CancelOrderItemCommand(orderId, Guid.NewGuid());

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(order);

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<OrderItemNotFoundException>();
            _orderRepository.DidNotReceive().Update(order);
            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Should_Rollback_Transaction_When_Exception_Occurs_During_Cancel()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = MockOrder(orderId, orderItemId);

            var command = new CancelOrderItemCommand(orderId, orderItemId);

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
               .Returns(order);

            _orderRepository.When(o => o.Update(order))
                 .Do(_ => throw new InvalidOperationException());           

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }

        private static Order MockOrder(Guid orderId, Guid orderItemId)
        {
            var item = new OrderItemBuilder()
                .WithId(orderItemId)
                .WithOrderId(orderId)
                .WithQuantity(3)
                .WithUnitPrice(20.00m)
                .Build();

            return new OrderBuilder()
               .WithId(orderId)
               .WithItem(item)
               .Build();
        }
    }
}
