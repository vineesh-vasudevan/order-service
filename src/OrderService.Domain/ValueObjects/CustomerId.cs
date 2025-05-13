namespace OrderService.Domain.ValueObjects
{
    public readonly record struct CustomerId
    {
        public Guid Value { get; }

        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("CustomerId cannot be empty.");

            Value = value;
        }

        public static CustomerId New() => new(Guid.NewGuid());

        public static CustomerId Of(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}