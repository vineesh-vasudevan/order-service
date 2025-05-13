using FluentValidation;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class AddressDtoValidator : AbstractValidator<AddressDto?>
    {
        public AddressDtoValidator()
        {
            When(x => x is not null, () =>
            {
                RuleFor(x => x!.FirstName)
                .NotEmpty().WithMessage("First name is required.");

                RuleFor(x => x!.LastName)
                    .NotEmpty().WithMessage("Last name is required.");

                RuleFor(x => x!.Street)
                    .NotEmpty().WithMessage("Street is required.");

                RuleFor(x => x!.City)
                    .NotEmpty().WithMessage("City is required.");

                RuleFor(x => x!.PostalCode)
                    .NotEmpty().WithMessage("Postal code is required.");

                RuleFor(x => x!.Country)
                    .NotEmpty().WithMessage("Country is required.");
            });
        }
    }
}