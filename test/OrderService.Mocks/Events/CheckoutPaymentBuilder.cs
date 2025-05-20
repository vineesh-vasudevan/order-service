using Basket.CheckOutEvent;

namespace OrderService.Mocks.Events
{
    public class CheckoutPaymentBuilder
    {
        private decimal _amount = 100.00m;
        private string _currency = "USD";
        private DateTime _paidAt = DateTime.UtcNow;
        private string _paymentMethod = "CreditCard";
        private bool _isSuccessful = true;
        private string _transactionId = Guid.NewGuid().ToString();

        public CheckoutPaymentBuilder WithAmount(decimal amount)
        { _amount = amount; return this; }

        public CheckoutPaymentBuilder WithCurrency(string currency)
        { _currency = currency; return this; }

        public CheckoutPaymentBuilder WithPaidAt(DateTime paidAt)
        { _paidAt = paidAt; return this; }

        public CheckoutPaymentBuilder WithPaymentMethod(string method)
        { _paymentMethod = method; return this; }

        public CheckoutPaymentBuilder WithIsSuccessful(bool isSuccess)
        { _isSuccessful = isSuccess; return this; }

        public CheckoutPaymentBuilder WithTransactionId(string transactionId)
        { _transactionId = transactionId; return this; }

        public CheckoutPayment Build()
        {
            return new CheckoutPayment
            {
                Amount = _amount,
                Currency = _currency,
                PaidAt = _paidAt,
                PaymentMethod = _paymentMethod,
                IsSuccessful = _isSuccessful,
                TransactionId = _transactionId
            };
        }

        public static CheckoutPayment Default() => new CheckoutPaymentBuilder().Build();
    }
}