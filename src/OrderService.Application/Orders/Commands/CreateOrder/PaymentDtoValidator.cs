namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto?>
    {
        public PaymentDtoValidator()
        {
            When(x => x is not null, () =>
            {
                RuleFor(x => x!.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than zero.");

                RuleFor(x => x!.Currency)
                    .NotEmpty().WithMessage("Currency is required.")
                    .Length(3).WithMessage("Currency must be a 3-letter ISO code.");

                RuleFor(x => x!.PaidAt)
                    .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("PaidAt cannot be in the future.");

                RuleFor(x => x!.PaymentMethod)
                    .NotEmpty().WithMessage("Payment method is required.");

                RuleFor(x => x!.TransactionId)
                    .NotEmpty().WithMessage("Transaction ID is required.");
            });
        }
    }
}