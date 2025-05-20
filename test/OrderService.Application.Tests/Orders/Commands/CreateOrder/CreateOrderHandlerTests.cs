using FluentAssertions;
using NSubstitute;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Dto.Input;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Mocks.Dto;

namespace OrderService.Application.Tests.Orders.Commands.CreateOrder
{
    [TestFixture]
    public class CreateOrderHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private CreateOrderHandler _sut;

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new CreateOrderHandler(_orderRepository, _unitOfWork);
        }

        [Test]
        public async Task Handle_ShouldCreateOrderAndCommit_WhenRequestIsValid()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var orderItems = new List<CreateOrderItemRequestDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductCode = "SKU-001",
                    Quantity = 2,
                    UnitPrice = 10,
                    TotalPrice = 20,
                    CreatedBy = "test-user"
                }
            };

            var dto = new CreateOrderRequestDtoBuilder()
                .WithId(orderId)
                .WithOrderName("My Special Order")
                .WithCustomerId(customerId)
                .WithCreatedBy("test-user")
                .WithOrderItems(orderItems)
                .Build();

            var command = new CreateOrderCommand(dto);

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(orderId);

            await _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            await _orderRepository.Received(1).AddAsync(Arg.Is<Order>(o =>
                o.CustomerId.Value == customerId &&
                o.OrderName.Value == "My Special Order" &&
                o.Items.Count == 1
            ), Arg.Any<CancellationToken>());

            await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public void Handle_ShouldRollbackAndThrow_WhenExceptionOccurs()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var orderItems = new List<CreateOrderItemRequestDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductCode = "SKU-001",
                    Quantity = 2,
                    UnitPrice = 10,
                    TotalPrice = 20,
                    CreatedBy = "test-user"
                }
            };

            var dto = new CreateOrderRequestDtoBuilder()
                .WithId(orderId)
                .WithOrderName("My Special Order")
                .WithCustomerId(customerId)
                .WithCreatedBy("test-user")
                .WithOrderItems(orderItems)
                .Build();

            var command = new CreateOrderCommand(dto);

            _orderRepository.When(r => r.AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()))
                .Do(_ => throw new InvalidOperationException("Simulated failure"));

            // Act
            Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Simulated failure");

            _unitOfWork.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
            _unitOfWork.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
            _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
        }
    }
}