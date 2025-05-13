namespace OrderService.Domain.ValueObjects
{
    public readonly record struct Currency
    {
        private static readonly HashSet<string> SupportedCodes = new(StringComparer.OrdinalIgnoreCase)
        {
            "USD", "EUR", "CHF", "INR"
        };

        public string Code { get; }

        private Currency(string code) => Code = code.ToUpperInvariant();

        public static Currency Of(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Currency code cannot be null or empty.");

            var upper = code.ToUpperInvariant();
            if (!SupportedCodes.Contains(upper))
                throw new DomainException($"Unsupported currency code: {code}");

            return new Currency(upper);
        }

        public override string ToString() => Code;

        public static Currency USD => new("USD");
        public static Currency EUR => new("EUR");
        public static Currency CHF => new("CHF");
        public static Currency INR => new("INR");
    }
}