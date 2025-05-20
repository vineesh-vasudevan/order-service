namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Request)
             .NotNull().WithMessage("Order details are required.");

            When(x => x.Request is not null, () =>
            {
                RuleFor(x => x.Request.Id)
                .NotEmpty().WithMessage("Order Id is required.");

                RuleFor(x => x.Request.CustomerId)
                    .NotEmpty().WithMessage("Customer Id is required.");

                RuleFor(x => x.Request.OrderName)
                    .NotEmpty().WithMessage("Order name is required.")
                    .MaximumLength(100);

                RuleFor(x => x.Request.ShippingAddress)
                    .NotNull().WithMessage("Shipping address is required.")
                    .SetValidator(new AddressDtoValidator());

                RuleFor(x => x.Request.BillingAddress)
                    .NotNull().WithMessage("Billing address is required.")
                    .SetValidator(new AddressDtoValidator());

                RuleFor(x => x.Request.Payment)
                    .NotNull().WithMessage("Payment details are required.")
                    .SetValidator(new PaymentDtoValidator());

                RuleFor(x => x.Request.OrderItems)
                    .NotEmpty().WithMessage("At least one order item is required.");

                RuleForEach(x => x.Request.OrderItems)
                    .SetValidator(new CreateOrderItemRequestDtoValidator());

                RuleFor(x => x.Request.CreatedBy)
                    .NotEmpty().WithMessage("CreatedBy is required.");
            });
        }
    }
}