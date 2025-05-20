using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.CancelOrder;

namespace OrderService.Application.Tests.Orders.Commands.CancelOrder
{
    [TestFixture]
    public class CancelOrderCommandValidatorTests
    {
        private CancelOrderCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CancelOrderCommandValidator();
        }

        [Test]
        public void Should_HaveValidationError_WhenIdIsEmpty()
        {
            // Arrange
            var command = new CancelOrderCommand(Guid.Empty);

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Order ID is required.");
        }

        [Test]
        public void Should_PassValidation_WhenIdIsValid()
        {
            // Arrange
            var command = new CancelOrderCommand(Guid.NewGuid());

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}