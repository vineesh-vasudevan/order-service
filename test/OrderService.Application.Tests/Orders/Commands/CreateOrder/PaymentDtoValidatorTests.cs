using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Application.Tests.Orders.Commands.CreateOrder
{
    [TestFixture]
    public class PaymentDtoValidatorTests
    {
        private PaymentDtoValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new PaymentDtoValidator();
        }

        [Test]
        public void Should_HaveValidationErrors_WhenFieldsAreInvalid()
        {
            // Arrange
            var dto = new PaymentDto
            {
                Amount = 0,
                Currency = "",
                PaidAt = DateTime.UtcNow.AddDays(1), // future date
                PaymentMethod = "",
                TransactionId = ""
            };

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x!.Amount)
                  .WithErrorMessage("Payment amount must be greater than zero.");

            result.ShouldHaveValidationErrorFor(x => x!.Currency)
                  .WithErrorMessage("Currency is required.");

            result.ShouldHaveValidationErrorFor(x => x!.Currency)
                  .WithErrorMessage("Currency must be a 3-letter ISO code.");

            result.ShouldHaveValidationErrorFor(x => x!.PaidAt)
                  .WithErrorMessage("PaidAt cannot be in the future.");

            result.ShouldHaveValidationErrorFor(x => x!.PaymentMethod)
                  .WithErrorMessage("Payment method is required.");

            result.ShouldHaveValidationErrorFor(x => x!.TransactionId)
                  .WithErrorMessage("Transaction ID is required.");
        }

        [Test]
        public void Should_PassValidation_WhenFieldsAreValid()
        {
            // Arrange
            var dto = new PaymentDto
            {
                Amount = 100.50m,
                Currency = "USD",
                PaidAt = DateTime.UtcNow.AddMinutes(-5),
                PaymentMethod = "CreditCard",
                TransactionId = "TX123456789"
            };

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}