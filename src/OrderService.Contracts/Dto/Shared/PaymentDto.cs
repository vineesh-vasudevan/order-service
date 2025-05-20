namespace OrderService.Contracts.Models.Shared
{
    public record PaymentDto
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } = default!;
        public DateTime PaidAt { get; init; }
        public string PaymentMethod { get; init; } = default!;
        public bool IsSuccessful { get; init; }
        public string TransactionId { get; init; } = default!;
    }
}