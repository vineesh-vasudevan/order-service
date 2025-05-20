using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Dto.Input;

namespace OrderService.Application.Tests.Orders.Commands.CreateOrder
{
    [TestFixture]
    public class CreateOrderItemRequestDtoValidatorTests
    {
        private CreateOrderItemRequestDtoValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CreateOrderItemRequestDtoValidator();
        }

        [Test]
        public void Should_HaveValidationErrors_WhenFieldsAreInvalid()
        {
            // Arrange
            var dto = new CreateOrderItemRequestDto
            {
                Id = Guid.Empty,
                OrderId = Guid.Empty,
                ProductCode = "",
                Quantity = 0,
                UnitPrice = -1,
                TotalPrice = -5,
                CreatedBy = ""
            };

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Item Id is required.");
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("OrderId is required.");
            result.ShouldHaveValidationErrorFor(x => x.ProductCode)
                  .WithErrorMessage("Product code is required.");
            result.ShouldHaveValidationErrorFor(x => x.Quantity)
                  .WithErrorMessage("Quantity must be a positive integer.");
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
                  .WithErrorMessage("Unit price must be non-negative.");
            result.ShouldHaveValidationErrorFor(x => x.TotalPrice)
                  .WithErrorMessage("Total price must be non-negative.");
            result.ShouldHaveValidationErrorFor(x => x.CreatedBy)
                  .WithErrorMessage("CreatedBy is required.");
        }

        [Test]
        public void Should_PassValidation_WhenFieldsAreValid()
        {
            // Arrange
            var dto = new CreateOrderItemRequestDto
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                ProductCode = "PROD-001",
                Quantity = 3,
                UnitPrice = 10.5m,
                TotalPrice = 31.5m,
                CreatedBy = "admin"
            };

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}