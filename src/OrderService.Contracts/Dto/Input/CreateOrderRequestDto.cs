using OrderService.Contracts.Dto.Enums;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Contracts.Dto.Input
{
    public record class CreateOrderRequestDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = default!;
        public AddressDto ShippingAddress { get; init; } = default!;
        public AddressDto BillingAddress { get; init; } = default!;
        public PaymentDto Payment { get; init; } = default!;
        public OrderStatusDto Status { get; init; }
        public List<CreateOrderItemRequestDto> OrderItems { get; init; } = new();
        public string CreatedBy { get; init; } = default!;
    }
}