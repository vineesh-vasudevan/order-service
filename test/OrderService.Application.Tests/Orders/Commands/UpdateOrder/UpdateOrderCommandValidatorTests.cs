using FluentValidation.TestHelper;
using OrderService.Application.Orders.Commands.UpdateOrder;
using OrderService.Contracts.Dto.Input;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Application.Tests.Orders.Commands.UpdateOrder
{
    [TestFixture]
    public class UpdateOrderCommandValidatorTests
    {
        private UpdateOrderCommandValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UpdateOrderCommandValidator();
        }

        [Test]
        public void Should_Have_Error_When_OrderId_Is_Empty()
        {
            //Arrange
            var command = new UpdateOrderCommand(Guid.Empty, new OrderPatchRequestDto { OrderName = "Test" });

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("OrderId is required.");
        }

        [Test]
        public void Should_Have_Error_When_Request_Is_Null()
        {
            //Arrange
            var command = new UpdateOrderCommand(Guid.NewGuid(), null!);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Request)
                  .WithErrorMessage("Order Details are required.");
        }

        [Test]
        public void Should_Have_Error_When_All_Fields_Are_Null()
        {
            //Arrange
            var command = new UpdateOrderCommand(Guid.NewGuid(), new OrderPatchRequestDto());

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor("Request")
                  .WithErrorMessage("At least one field must be provided to update.");
        }

        [Test]
        public void Should_Not_Have_Error_When_At_Least_One_Field_Is_Present()
        {
            //Arrange
            var dto = new OrderPatchRequestDto
            {
                OrderName = "Updated Order"
            };
            var command = new UpdateOrderCommand(Guid.NewGuid(), dto);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor("Request");
        }

        [Test]
        public void Should_Trigger_AddressDtoValidator_For_ShippingAddress()
        {
            //Arrange
            var dto = new OrderPatchRequestDto
            {
                ShippingAddress = new AddressDto
                {
                }
            };
            var command = new UpdateOrderCommand(Guid.NewGuid(), dto);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveAnyValidationError();
            result.ShouldHaveValidationErrorFor("Request.ShippingAddress.FirstName");
        }

        [Test]
        public void Should_Trigger_PaymentValidator_When_Payment_Is_Present()
        {
            //Arrange
            var dto = new OrderPatchRequestDto
            {
                Payment = new PaymentDto()
            };

            var command = new UpdateOrderCommand(Guid.NewGuid(), dto);

            //Act
            var result = _sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor("Request.Payment.Amount");
        }
    }
}