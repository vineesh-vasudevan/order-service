using OrderService.Application.Orders.Commands.CreateOrder;

namespace OrderService.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
               .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("Order Details are required.");

            When(x => x.Request is not null, () =>
            {
                RuleFor(x => x.Request)
                .Must(HaveAtLeastOneNonNullProperty)
                .WithMessage("At least one field must be provided to update.");

                RuleFor(x => x.Request!.ShippingAddress)
                    .SetValidator(new AddressDtoValidator())
                    .When(x => x.Request is not null && x.Request.ShippingAddress is not null);

                RuleFor(x => x.Request!.BillingAddress)
                    .SetValidator(new AddressDtoValidator())
                    .When(x => x.Request is not null && x.Request.ShippingAddress is not null);

                RuleFor(x => x.Request!.Payment)
                   .SetValidator(new PaymentDtoValidator())
                   .When(x => x.Request is not null && x.Request.Payment is not null);
            });
        }

        private static bool HaveAtLeastOneNonNullProperty(OrderPatchRequestDto? dto)
        {
            if (dto is null) return false;

            return dto.OrderName != null
                || dto.ShippingAddress != null
                || dto.BillingAddress != null
                || dto.Payment != null
                || dto.Status != null;
        }
    }
}