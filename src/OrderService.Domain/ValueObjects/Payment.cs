
namespace OrderService.Domain.ValueObjects
{
    public sealed record Payment(
        decimal Amount,
        Currency Currency,
        DateTime PaidAt,
        string PaymentMethod,
        bool IsSuccessful,
        string TransactionId
    )
    {
        public static Payment Create(
            decimal amount,
            Currency currency,
            DateTime paidAt,
            string paymentMethod,
            bool isSuccessful,
            string transactionId)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException("Payment method is required.", nameof(paymentMethod));
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("Transaction ID is required.", nameof(transactionId));

            return new Payment(
                amount,
                currency,
                paidAt,
                paymentMethod.Trim(),
                isSuccessful,
                transactionId.Trim()
            );
        }

        public override string ToString() =>
            $"{Amount} {Currency} via {PaymentMethod} at {PaidAt:u} (Success: {IsSuccessful})";
    }

}
