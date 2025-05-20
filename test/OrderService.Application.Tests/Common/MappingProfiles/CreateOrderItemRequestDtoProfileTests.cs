using AutoMapper;
using Basket.CheckOutEvent;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Dto.Input;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class CreateOrderItemRequestDtoProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateOrderItemRequestDtoProfile>();
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
        public void Should_Map_BasketItemEvent_To_CreateOrderItemRequestDto_With_External_OrderId_And_CreatedBy()
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

            var orderId = Guid.NewGuid();
            var createdBy = "testuser@example.com";

            // Act
            var result = _mapper.Map<CreateOrderItemRequestDto>((basketItem, orderId, createdBy));

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(basketItem.Id);
            result.ProductCode.Should().Be(basketItem.ProductCode);
            result.Quantity.Should().Be(basketItem.Quantity);
            result.UnitPrice.Should().Be(basketItem.Price);
            result.TotalPrice.Should().Be(basketItem.TotalPrice);
            result.OrderId.Should().Be(orderId);
            result.CreatedBy.Should().Be(createdBy);
        }
    }
}