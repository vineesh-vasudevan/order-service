using FluentValidation;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CreateOrderRequest.Id)
                .NotEmpty().WithMessage("Order Id is required.");

            RuleFor(x => x.CreateOrderRequest.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(x => x.CreateOrderRequest.OrderName)
                .NotEmpty().WithMessage("Order name is required.")
                .MaximumLength(100);

            RuleFor(x => x.CreateOrderRequest.ShippingAddress)
                .NotNull().WithMessage("Shipping address is required.")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.CreateOrderRequest.BillingAddress)
                .NotNull().WithMessage("Billing address is required.")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.CreateOrderRequest.Payment)
                .NotNull().WithMessage("Payment details are required.")
                .SetValidator(new PaymentDtoValidator());

            RuleFor(x => x.CreateOrderRequest.OrderItems)
                .NotEmpty().WithMessage("At least one order item is required.");

            RuleForEach(x => x.CreateOrderRequest.OrderItems)
                .SetValidator(new CreateOrderItemRequestDtoValidator());

            RuleFor(x => x.CreateOrderRequest.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");
        }
    }
}