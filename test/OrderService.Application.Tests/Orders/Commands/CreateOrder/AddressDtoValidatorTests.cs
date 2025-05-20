using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Application.Tests.Orders.Commands.CreateOrder
{
    [TestFixture]
    public class AddressDtoValidatorTests
    {
        private AddressDtoValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new AddressDtoValidator();
        }

        [Test]
        public void Should_HaveErrors_WhenPropertiesAreEmpty()
        {
            // Arrange
            var dto = new AddressDto();

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x!.FirstName);
            result.ShouldHaveValidationErrorFor(x => x!.LastName);
            result.ShouldHaveValidationErrorFor(x => x!.Street);
            result.ShouldHaveValidationErrorFor(x => x!.City);
            result.ShouldHaveValidationErrorFor(x => x!.PostalCode);
            result.ShouldHaveValidationErrorFor(x => x!.Country);
        }

        [Test]
        public void Should_PassValidation_WhenAllPropertiesAreValid()
        {
            // Arrange
            var dto = new AddressDto
            {
                FirstName = "John",
                LastName = "Doe",
                Street = "123 Main St",
                City = "Zurich",
                PostalCode = "8000",
                Country = "Switzerland"
            };

            // Act
            var result = _sut.TestValidate(dto);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}