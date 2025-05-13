using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Models.Enums;
using OrderService.Domain.Enums;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class OrderStatusToDtoConverterTests
    {
        private OrderStatusToDtoConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new OrderStatusToDtoConverter();
        }

        [TestCase("Pending", OrderStatusDto.Pending)]
        [TestCase("Confirmed", OrderStatusDto.Confirmed)]
        [TestCase("Shipped", OrderStatusDto.Shipped)]
        [TestCase("Delivered", OrderStatusDto.Delivered)]
        [TestCase("Cancelled", OrderStatusDto.Cancelled)]
        public void Convert_Should_Return_Correct_OrderStatusDto(string smartEnumName, OrderStatusDto expectedDto)
        {
            // Arrange
            var smartEnum = OrderStatus.FromName(smartEnumName);

            // Act
            var result = _converter.Convert(smartEnum, context: null!);

            // Assert
            result.Should().Be(expectedDto);
        }
    }
}