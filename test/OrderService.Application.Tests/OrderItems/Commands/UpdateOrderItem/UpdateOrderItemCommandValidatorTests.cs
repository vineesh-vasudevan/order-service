using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.OrderItems.Commands.UpdateOrderItem;
using OrderService.Contracts.Dto.Input;

namespace OrderService.Application.Tests.OrderItems.Commands.UpdateOrderItem
{
    [TestFixture]
    public class UpdateOrderItemCommandValidatorTests
    {
        private UpdateOrderItemCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UpdateOrderItemCommandValidator();
        }

        [Test]
        public void Should_HaveValidationError_WhenFieldsAreMissing()
        {
            // Arrange
            var command = new UpdateOrderItemCommand(Guid.Empty, Guid.Empty, null!);

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
            result.ShouldHaveValidationErrorFor(c => c.OrderItemId);
            result.ShouldHaveValidationErrorFor(c => c.Request);
        }

        [Test]
        public void Should_HaveValidationErrors_WhenRequestFieldsAreInvalid()
        {
            // Arrange
            var request = new OrderItemPatchRequestDto
            {
                Quantity = 0,
            };

            var command = new UpdateOrderItemCommand(Guid.Empty, Guid.Empty, request);

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
            result.ShouldHaveValidationErrorFor(c => c.OrderItemId);
            result.ShouldHaveValidationErrorFor(c => c.Request.Quantity);
        }

        [Test]
        public void Should_PassValidation_WhenAllFieldsAreValid()
        { // Arrange
            var request = new OrderItemPatchRequestDto
            {
                Quantity = 1,
            };

            var command = new UpdateOrderItemCommand(Guid.NewGuid(), Guid.NewGuid(), request);

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}