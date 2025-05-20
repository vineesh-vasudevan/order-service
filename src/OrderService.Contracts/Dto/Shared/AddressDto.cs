namespace OrderService.Contracts.Models.Shared
{
    public record AddressDto
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string City { get; init; } = default!;
        public string? State { get; init; }
        public string PostalCode { get; init; } = default!;
        public string Country { get; init; } = default!;
    }
}