using AutoMapper;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Models.Enums;
using OrderService.Contracts.Models.Output;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class OrderItemProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderItemProfile>();
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
        public void Should_Map_OrderItem_To_OrderItemDto_Correctly()
        {
            // Arrange
            var orderItem = OrderItem.Create(
                id: OrderItemId.New(),
                orderId: OrderId.Of(Guid.NewGuid()),
                productCode: "ABC123",
                quantity: 5,
                unitPrice: 10.5m,
                totalPrice: 52.5m
            );

            orderItem.SetAudit("System", true);

            // Act
            var result = _mapper.Map<OrderItemDto>(orderItem);

            // Assert
            result.Should().NotBeNull();
            result.OrderId.Should().Be(orderItem.OrderId.Value);
            result.ProductCode.Should().Be(orderItem.ProductCode);
            result.Quantity.Should().Be(orderItem.Quantity);
            result.UnitPrice.Should().Be(orderItem.UnitPrice);
            result.TotalPrice.Should().Be(orderItem.TotalPrice);
            result.Status.Should().Be((OrderItemStatusDto)orderItem.Status.Value);

            result.CreatedAt.Should().Be(orderItem.CreatedAt);
            result.CreatedBy.Should().Be(orderItem.CreatedBy);
            result.LastModifiedAt.Should().Be(orderItem.LastModifiedAt);
            result.LastModifiedBy.Should().Be(orderItem.LastModifiedBy);
        }
    }
}