using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.Orders.Commands.UpdateOrder;
using OrderService.Contracts.Dto.Input;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Mocks.Domain;

namespace OrderService.Application.Tests.Orders.Commands.UpdateOrder
{
    [TestFixture]
    public class UpdateOrderHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private UpdateOrderHandler _sut;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new UpdateOrderHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Handle_ShouldUpdateOrderAndCommitTransaction()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();

            var order = OrderMocks.MockOrder(orderId, orderItemId);

            var requestDto = new OrderPatchRequestDto
            {
                OrderName = "New Name"
            };

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(order);

            var command = new UpdateOrderCommand(orderId, requestDto);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _orderRepository.Received(1).Update(order);
            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());

            order.OrderName.Value.Should().Be(requestDto.OrderName);
        }

        [Test]
        public async Task Handle_ShouldThrowOrderNotFoundException_WhenOrderNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(Maybe<Order>.None);

            var command = new UpdateOrderCommand(orderId, new OrderPatchRequestDto());

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<OrderNotFoundException>()
                     .WithMessage($"*{orderId}*");

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Handle_ShouldRollback_WhenExceptionOccursDuringUpdate()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();

            var order = OrderMocks.MockOrder(orderId, orderItemId);

            _orderRepository.GetByIdAsync(orderId, Arg.Any<CancellationToken>())
                .Returns(order);

            _orderRepository.When(r => r.Update(order)).Do(_ => throw new Exception("DB error"));

            var command = new UpdateOrderCommand(orderId, new OrderPatchRequestDto
            {
                OrderName = "OrderName"
            });

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                     .WithMessage("DB error");

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }
    }
}