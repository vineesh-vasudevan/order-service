using FluentAssertions;
using FluentValidation.TestHelper;
using OrderService.Application.OrderItems.Commands.CancelOrderItem;

namespace OrderService.Application.Tests.OrderItems.Commands.CancelOrderItem
{
    [TestFixture]
    public class CancelOrderItemCommandValidatorTests
    {
        private CancelOrderItemCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CancelOrderItemCommandValidator();
        }

        [Test]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new CancelOrderItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Should_Fail_When_OrderId_Is_Empty()
        {
            var command = new CancelOrderItemCommand(Guid.Empty, Guid.NewGuid());

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("OrderId must not be the default GUID.");
        }

        [Test]
        public void Should_Fail_When_OrderItemId_Is_Empty()
        {
            var command = new CancelOrderItemCommand(Guid.NewGuid(), Guid.Empty);

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItemId)
                  .WithErrorMessage("OrderItemId must not be the default GUID.");
        }

        [Test]
        public void Should_Fail_When_Both_OrderId_And_OrderItemId_Are_Empty()
        {
            var command = new CancelOrderItemCommand(Guid.Empty, Guid.Empty);

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("OrderId must not be the default GUID.");
            result.ShouldHaveValidationErrorFor(x => x.OrderItemId)
                  .WithErrorMessage("OrderItemId must not be the default GUID.");
        }
    }
}