using FluentValidation;

namespace OrderService.Application.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.")
                .Must(id => id != Guid.Empty).WithMessage("OrderId must not be default GUID.");

            RuleFor(x => x.OrderItemId)
                .NotEmpty().WithMessage("OrderItemId is required.")
                .Must(id => id != Guid.Empty).WithMessage("OrderItemId must not be default GUID.");

            RuleFor(x => x.Request)
             .NotNull().WithMessage("Order item details are required.");

            RuleFor(x => x.Request.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}