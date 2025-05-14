using FluentValidation;

namespace OrderService.Application.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId)
               .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("Order item is required.");

            When(x => x.Request is not null, () =>
            {
                RuleFor(x => x.Request.Id)
                    .NotEmpty().WithMessage("Item Id is required.");

                RuleFor(x => x.Request.ProductCode)
                    .NotEmpty().WithMessage("Product code is required.");

                RuleFor(x => x.Request.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be a positive integer.");

                RuleFor(x => x.Request.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

                RuleFor(x => x.Request.TotalPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Total price must be non-negative.");

                RuleFor(x => x.Request.CreatedBy)
                    .NotEmpty().WithMessage("CreatedBy is required.");
            });
        }
    }
}