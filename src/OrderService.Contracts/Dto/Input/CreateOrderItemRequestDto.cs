namespace OrderService.Contracts.Dto.Input
{
    public record CreateOrderItemRequestDto
    {
        public Guid Id { get; init; }
        public Guid OrderId { get; init; }
        public string ProductCode { get; init; } = default!;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
        public string CreatedBy { get; init; } = default!;
    }
}