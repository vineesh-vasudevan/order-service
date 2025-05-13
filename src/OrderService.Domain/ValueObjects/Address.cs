namespace OrderService.Domain.ValueObjects
{
    public sealed record Address(
        string FirstName,
        string LastName,
        string Street,
        string City,
        string? State,
        string PostalCode,
        string Country
    )
    {
        public static Address Create(
            string firstName,
            string lastName,
            string street,
            string city,
            string? state,
            string postalCode,
            string country)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First Name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("LastName Name is required.");
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code is required.");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required.");

            return new Address(
                firstName,
                lastName,
                street,
                city,
                state,
                postalCode,
                country
            );
        }
    }
}