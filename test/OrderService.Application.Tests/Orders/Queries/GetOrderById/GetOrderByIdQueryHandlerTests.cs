using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Application.Orders.Queries.GetOrderById;
using OrderService.Contracts.Dto.Enums;
using OrderService.Contracts.Dto.Output;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Domain.ValueObjects;
using OrderService.Mocks.Domain;

namespace OrderService.Application.Tests.Orders.Queries.GetOrderById
{
    [TestFixture]
    public class GetOrderByIdQueryHandlerTests
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private GetOrderByIdQueryHandler _sut;
        private static readonly Guid OrderGuid = Guid.Parse("33333333-3333-3333-3333-333333333333");
        private static readonly Guid OrderItemGuid = Guid.Parse("44444444-4444-4444-4444-444444444444");

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<OrderItemProfile>();
                cfg.AddProfile<PaymentProfile>();
            });

            _mapper = config.CreateMapper();
            _orderRepository = Substitute.For<IOrderRepository>();
            _sut = new GetOrderByIdQueryHandler(_orderRepository, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnMappedOrder_WhenOrderExists()
        {
            // Arrange

            var order = OrderMocks.MockOrder(OrderGuid, OrderItemGuid);

            _orderRepository.GetByIdAsync(OrderGuid, Arg.Any<CancellationToken>())
                .Returns(order);

            var query = new GetOrderByIdQuery(OrderGuid);

            // Act
            var result = await _sut.Handle(query, CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).GetByIdAsync(OrderGuid, Arg.Any<CancellationToken>());

            result.Should().NotBeNull();
            result.Id.Should().Be(order.Id.Value);
            result.CustomerId.Should().Be(order.CustomerId.Value);
            result.OrderName.Should().Be(order.OrderName.Value);

            AssertAddressMap(order.ShippingAddress, result.ShippingAddress);
            AssertAddressMap(order.BillingAddress, result.BillingAddress);

            result.OrderItems.Should().NotBeNull();
            result.OrderItems.Count.Should().Be(order.Items.Count);

            result.Status.Should().Be((OrderStatusDto)order.Status.Value);

            var item = order.GetOrderItem(OrderItemGuid);

            AssertOrderItemMap(item.Value, result.OrderItems[0]);

            AssertPaymentMap(order.Payment, result.Payment);

            result.CreatedAt.Should().Be(order.CreatedAt);
            result.CreatedBy.Should().Be(order.CreatedBy);
            result.LastModifiedAt.Should().Be(order.LastModifiedAt);
            result.LastModifiedBy.Should().Be(order.LastModifiedBy);
            result.TotalPrice.Should().Be(order.TotalPrice);
        }

        private static void AssertAddressMap(Address source, AddressDto destination)
        {
            destination.Should().NotBeNull();
            destination.FirstName.Should().Be(source.FirstName);
            destination.LastName.Should().Be(source.LastName);
            destination.Street.Should().Be(source.Street);
            destination.City.Should().Be(source.City);
            destination.State.Should().Be(source.State);
            destination.PostalCode.Should().Be(source.PostalCode);
            destination.Country.Should().Be(source.Country);
        }

        private static void AssertOrderItemMap(OrderItem source, OrderItemDto destination)
        {
            destination.Id.Should().Be(OrderItemGuid);
            destination.OrderId.Should().Be(OrderGuid);
            destination.ProductCode.Should().Be(source.ProductCode);
            destination.Quantity.Should().Be(source.Quantity);
            destination.UnitPrice.Should().Be(source.UnitPrice);
            destination.TotalPrice.Should().Be(source.TotalPrice);
            destination.Status.Should().Be((OrderItemStatusDto)source.Status.Value);
            destination.CreatedAt.Should().Be(source.CreatedAt);
            destination.CreatedBy.Should().Be(source.CreatedBy);
            destination.LastModifiedAt.Should().Be(source.LastModifiedAt);
            destination.LastModifiedBy.Should().Be(source.LastModifiedBy);
        }

        private static void AssertPaymentMap(Payment source, PaymentDto destination)
        {
            destination.Amount.Should().Be(source.Amount);
            destination.Currency.Should().Be(source.Currency.Code);
            destination.PaidAt.Should().Be(source.PaidAt);
            destination.PaymentMethod.Should().Be(source.PaymentMethod);
            destination.IsSuccessful.Should().Be(source.IsSuccessful);
            destination.TransactionId.Should().Be(source.TransactionId.Value);
        }
    }
}