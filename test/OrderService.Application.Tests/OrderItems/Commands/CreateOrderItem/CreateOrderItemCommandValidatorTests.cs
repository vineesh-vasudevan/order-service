using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.OrderItems.Commands.CreateOrderItem;
using OrderService.Contracts.Models.Input;

namespace OrderService.Application.Tests.OrderItems.Commands.CreateOrderItem
{
    [TestFixture]
    public class CreateOrderItemCommandValidatorTests
    {
        private CreateOrderItemCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CreateOrderItemCommandValidator();
        }

        [Test]
        public void Should_HaveValidationError_WhenFieldsAreMissing()
        {
            // Arrange
            var command = new CreateOrderItemCommand(null!, Guid.Empty);

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
            result.ShouldHaveValidationErrorFor(c => c.Request);
        }

        [Test]
        public void Should_HaveValidationErrors_WhenRequestFieldsAreInvalid()
        {
            // Arrange
            var request = new CreateOrderItemRequestDto
            {
                Id = Guid.Empty,
                ProductCode = "",
                Quantity = 0,
                UnitPrice = -1,
                TotalPrice = -10,
                CreatedBy = ""
            };

            var command = new CreateOrderItemCommand(request, Guid.NewGuid());

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Request.Id);
            result.ShouldHaveValidationErrorFor(c => c.Request.ProductCode);
            result.ShouldHaveValidationErrorFor(c => c.Request.Quantity);
            result.ShouldHaveValidationErrorFor(c => c.Request.UnitPrice);
            result.ShouldHaveValidationErrorFor(c => c.Request.TotalPrice);
            result.ShouldHaveValidationErrorFor(c => c.Request.CreatedBy);
        }

        [Test]
        public void Should_PassValidation_WhenAllFieldsAreValid()
        {
            // Arrange
            var request = new CreateOrderItemRequestDto
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                ProductCode = "P100",
                Quantity = 3,
                UnitPrice = 10.5m,
                TotalPrice = 31.5m,
                CreatedBy = "test.user"
            };

            var command = new CreateOrderItemCommand(request, Guid.NewGuid());

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
