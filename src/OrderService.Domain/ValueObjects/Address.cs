
namespace OrderService.Domain.ValueObjects
{
    public sealed record Address(
        string Street,
        string City,
        string State,
        string PostalCode,
        string Country,
        string EmailAddress
    )
    {
        public static Address Create(string street, string city, string state, string postalCode, string country, string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code is required.");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required.");
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentException("Email Address is required.");

            return new Address(
                street,
                city,
                state?.Trim() ?? "",
                postalCode,
                country,
                emailAddress
            );
        }
    }

}
