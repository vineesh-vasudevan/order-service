using OrderService.Contracts.Dto.Enums;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Contracts.Dto.Output
{
    public record OrderDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; } = default!;
        public AddressDto BillingAddress { get; init; } = default!;
        public PaymentDto Payment { get; init; } = default!;
        public OrderStatusDto Status { get; init; }
        public List<OrderItemDto> OrderItems { get; init; } = new();
        public string CreatedBy { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string LastModifiedBy { get; init; } = string.Empty;
        public DateTime LastModifiedAt { get; init; }
        public decimal TotalPrice { get; init; }
    }
}