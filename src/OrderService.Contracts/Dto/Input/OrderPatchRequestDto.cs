using OrderService.Contracts.Dto.Enums;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Contracts.Dto.Input
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