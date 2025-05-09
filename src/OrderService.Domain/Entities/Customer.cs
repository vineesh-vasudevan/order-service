
namespace OrderService.Domain.Entities
{
    public class Customer : Entity<CustomerId>
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public static Customer Create(
            CustomerId id,
            string firstName,
            string lastName,
            string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");

            return new Customer
            {
                Id = id,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };
        }

        public override string ToString() => $"{FirstName} {LastName} ({Email})";
    }
}
