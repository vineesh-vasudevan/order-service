namespace OrderService.Application.OrderItems.Commands.CancelOrderItem
{
    public class CancelOrderItemCommandValidator : AbstractValidator<CancelOrderItemCommand>
    {
        public CancelOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId must not be empty.")
                .Must(id => id != Guid.Empty).WithMessage("OrderId must not be the default GUID.");

            RuleFor(x => x.OrderItemId)
                .NotEmpty().WithMessage("OrderItemId must not be empty.")
                .Must(id => id != Guid.Empty).WithMessage("OrderItemId must not be the default GUID.");
        }
    }
}