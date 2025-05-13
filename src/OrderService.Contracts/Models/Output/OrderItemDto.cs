using OrderService.Contracts.Models.Enums;

namespace OrderService.Contracts.Models.Output
{
    public record OrderItemDto
    {
        public Guid Id { get; init; }
        public Guid OrderId { get; init; }
        public string ProductCode { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
        public OrderItemStatusDto Status { get; init; }
        public string CreatedBy { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string LastModifiedBy { get; init; } = string.Empty;
        public DateTime LastModifiedAt { get; init; }
    }
}