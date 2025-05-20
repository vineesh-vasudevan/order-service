using AutoMapper;
using Basket.CheckOutEvent;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Dto.Input;
using OrderService.Mocks.Events;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class CreateOrderRequestDtoProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateOrderItemRequestDtoProfile>();
                cfg.AddProfile<CreateOrderRequestDtoProfile>();
                cfg.AddProfile<PaymentDtoProfile>();
                cfg.AddProfile<AddressDtoProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_have_valid_configuration()
        {
            //Assert
            _mapper
                .ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_BasketCheckoutEvent_To_CreateOrderRequestDto_Using_Context_Values()
        {
            // Arrange

            var basketItem = new BasketItemEvent
            {
                Id = Guid.NewGuid(),
                ProductCode = "PRD-123",
                Color = "Red",
                Price = 10.5m,
                Quantity = 2,
                BasketId = Guid.NewGuid(),
                Status = CheckoutItemStatus.Active,
                TotalPrice = 21.0m
            };

            var checkoutEvent = new BasketCheckoutEvent
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderName = "OriginalOrder",
                ShippingAddress = CheckoutAddressBuilder.Default(),
                BillingAddress = CheckoutAddressBuilder.Default(),
                Payment = CheckoutPaymentBuilder.Default(),
                Status = BasketCheckoutStatus.CheckedOut,
                Items = [basketItem]
            };

            var orderId = Guid.NewGuid();
            var createdBy = "testuser@example.com";

            // Act
            var result = _mapper.Map<CreateOrderRequestDto>(checkoutEvent, opts =>
            {
                opts.Items["OrderId"] = orderId;
                opts.Items["CreatedBy"] = createdBy;
            }) with
            {
                OrderItems = checkoutEvent.Items
                    .Select(item => _mapper.Map<CreateOrderItemRequestDto>((item, orderId, createdBy)))
                    .ToList()
            };

            var orderItem = result.OrderItems[0];

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(orderId);
            result.CreatedBy.Should().Be(createdBy);
            result.OrderName.Should().Be(checkoutEvent.OrderName);
            result.CustomerId.Should().Be(checkoutEvent.CustomerId);
            result.ShippingAddress.Should().BeEquivalentTo(checkoutEvent.ShippingAddress);
            result.BillingAddress.Should().BeEquivalentTo(checkoutEvent.BillingAddress);
            result.Payment.Should().BeEquivalentTo(checkoutEvent.Payment);
            result.OrderItems.Count.Should().Be(1);

            orderItem.Id.Should().Be(basketItem.Id);
            orderItem.ProductCode.Should().Be(basketItem.ProductCode);
            orderItem.Quantity.Should().Be(basketItem.Quantity);
            orderItem.UnitPrice.Should().Be(basketItem.Price);
            orderItem.TotalPrice.Should().Be(basketItem.TotalPrice);
            orderItem.OrderId.Should().Be(orderId);
            orderItem.CreatedBy.Should().Be(createdBy);
        }
    }
}