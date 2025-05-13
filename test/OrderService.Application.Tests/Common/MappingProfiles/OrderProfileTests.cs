using AutoMapper;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Models.Enums;
using OrderService.Contracts.Models.Output;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class OrderProfileTests
    {
        private IMapper _mapper;

        private static readonly Guid OrderGuid = Guid.Parse("33333333-3333-3333-3333-333333333333");
        private static readonly Guid CustomerGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid OrderItemGuid = Guid.Parse("44444444-4444-4444-4444-444444444444");

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<OrderItemProfile>();
                cfg.AddProfile<PaymentProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_Have_Valid_Configuration()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_Order_To_OrderDto_Correctly()
        {
            // Arrange
            var order = CreateSampleOrder();

            // Act
            var result = _mapper.Map<OrderDto>(order);

            // Assert
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

        private Order CreateSampleOrder()
        {
            var orderId = OrderId.Of(OrderGuid);
            var customerId = CustomerId.Of(CustomerGuid);

            var order = Order.Create(
                id: orderId,
                customerId: customerId,
                orderName: new OrderName("First Order"),
                shippingAddress: CreateSampleAddress(),
                billingAddress: CreateSampleAddress(),
                payment: Payment.Create(
                    amount: 129.98m,
                    currency: Currency.USD,
                    paidAt: DateTime.UtcNow,
                    paymentMethod: "Credit Card",
                    isSuccessful: true,
                    transactionId: TransactionId.Of("TXN123456")
                )
            );
            order.SetAudit("System", true);

            var orderItem = OrderItem.Create(
                id: OrderItemId.Of(OrderItemGuid),
                orderId: orderId,
                productCode: "PROD001",
                quantity: 1,
                unitPrice: 49.99m,
                totalPrice: 49.99m
            );

            orderItem.SetAudit("System", true);

            order.Add(orderItem);

            return order;
        }

        private Address CreateSampleAddress()
        {
            return Address.Create("John", "Wick", "123 Main St", "CityA", "StateA", "10001", "US");
        }
    }
}