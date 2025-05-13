using FluentValidation;
using OrderService.Contracts.Models.Input;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderItemRequestDtoValidator : AbstractValidator<CreateOrderItemRequestDto>
    {
        public CreateOrderItemRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Item Id is required.");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Product code is required.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .Must(q => q > 0)
                .WithMessage("Quantity must be a positive integer.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Total price must be non-negative.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");
        }
    }
}