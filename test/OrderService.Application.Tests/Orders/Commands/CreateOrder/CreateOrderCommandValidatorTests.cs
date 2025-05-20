using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Dto.Input;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Tests.Orders.Commands.CreateOrder
{
    [TestFixture]
    public class CreateOrderCommandValidatorTests
    {
        private CreateOrderCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CreateOrderCommandValidator();
        }

        [Test]
        public void Should_HaveErrors_WhenRequestIsNull()
        {
            var command = new CreateOrderCommand(null!);

            var result = _sut.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Request)
                  .WithErrorMessage("Order details are required.");
        }

        [Test]
        public void Should_HaveErrors_WhenRequestFieldsAreInvalid()
        {
            var dto = new CreateOrderRequestDto
            {
                Id = Guid.Empty,
                CustomerId = Guid.Empty,
                OrderName = "",
                ShippingAddress = new AddressDto(),
                BillingAddress = new AddressDto(),
                Payment = new PaymentDto(),
                OrderItems = [],
                CreatedBy = ""
            };

            var command = new CreateOrderCommand(dto);

            var result = _sut.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Request.Id);
            result.ShouldHaveValidationErrorFor(x => x.Request.CustomerId);
            result.ShouldHaveValidationErrorFor(x => x.Request.OrderName);
            result.ShouldHaveValidationErrorFor(x => x.Request.ShippingAddress!.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.Request.BillingAddress!.LastName);
            result.ShouldHaveValidationErrorFor(x => x.Request.Payment.Amount);
            result.ShouldHaveValidationErrorFor(x => x.Request.OrderItems);
            result.ShouldHaveValidationErrorFor(x => x.Request.CreatedBy);
        }

        [Test]
        public void Should_PassValidation_WhenAllFieldsAreValid()
        {
            //Arrange
            var dto = new CreateOrderRequestDto
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderName = "Valid Order",
                ShippingAddress = new AddressDto
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Street = "123 Main St",
                    City = "Zurich",
                    PostalCode = "8000",
                    Country = "Switzerland"
                },
                BillingAddress = new AddressDto
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Street = "456 Side St",
                    City = "Bern",
                    PostalCode = "3000",
                    Country = "Switzerland"
                },
                Payment = new PaymentDto
                {
                    Currency = Currency.USD.Code,
                    PaymentMethod = "CreditCard",
                    Amount = 99.99m,
                    IsSuccessful = true,
                    PaidAt = DateTime.UtcNow.AddDays(-3),
                    TransactionId = "TXN123456"
                },
                OrderItems = new List<CreateOrderItemRequestDto>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = Guid.NewGuid(),
                        ProductCode = "SKU123",
                        Quantity = 2,
                        UnitPrice = 10,
                        TotalPrice = 20,
                        CreatedBy = "admin"
                    }
                },
                CreatedBy = "system"
            };

            var command = new CreateOrderCommand(dto);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}