using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.OrderItems.Commands.UpdateOrderItem;
using OrderService.Contracts.Dto.Input;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Mocks.Domain;

namespace OrderService.Application.Tests.OrderItems.Commands.UpdateOrderItem
{
    [TestFixture]
    public class UpdateOrderItemHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private UpdateOrderItemHandler _sut;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new UpdateOrderItemHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Should_Update_OrderItem_And_Commit_Transaction_When_Order_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = OrderMocks.MockOrder(orderId, orderItemId);

            var expectedTotalPrice = 20.00m;
            var expectedItemTotalPrice = 20.00m;

            var request = new OrderItemPatchRequestDto
            {
                Quantity = 1,
            };

            var command = new UpdateOrderItemCommand(orderId, orderItemId, request);

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(order);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _orderRepository.Received(1).Update(order);
            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
            foreach (var item in order.Items)
            {
                item.Quantity.Should().Be(request.Quantity);
                item.TotalPrice.Should().Be(expectedItemTotalPrice);
            }
            order.TotalPrice.Should().Be(expectedTotalPrice);
        }

        [Test]
        public async Task Should_Throw_OrderNotFoundException_And_Rollback_When_Order_Not_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var request = new OrderItemPatchRequestDto
            {
                Quantity = 1,
            };

            var command = new UpdateOrderItemCommand(orderId, orderItemId, request);

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
            var order = OrderMocks.MockOrder(orderId, orderItemId);

            var request = new OrderItemPatchRequestDto
            {
                Quantity = 1,
            };

            var command = new UpdateOrderItemCommand(orderId, Guid.NewGuid(), request);

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
        public async Task Should_Rollback_Transaction_When_Exception_Occurs_During_UpdatingOrderItem()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var order = OrderMocks.MockOrder(orderId, orderItemId);

            var request = new OrderItemPatchRequestDto
            {
                Quantity = 1,
            };

            var command = new UpdateOrderItemCommand(orderId, orderItemId, request);

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
    }
}