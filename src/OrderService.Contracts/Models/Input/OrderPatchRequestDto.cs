using OrderService.Contracts.Models.Enums;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Contracts.Models.Input
{
    public class OrderPatchRequestDto
    {
        public string? OrderName { get; init; }
        public AddressDto? ShippingAddress { get; init; }
        public AddressDto? BillingAddress { get; init; }
        public PaymentDto? Payment { get; init; }
        public OrderStatusDto? Status { get; init; }
    }
}