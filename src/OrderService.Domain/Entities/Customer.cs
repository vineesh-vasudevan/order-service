
namespace OrderService.Domain.Entities
{
    public class Customer : Entity<CustomerId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        private Customer(
            CustomerId id,
            string firstName,
            string lastName,
            string email,
            DateTime createdAt,
            DateTime lastModifiedAt,
            string createdBy,
            string lastModifiedBy)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(createdBy)) throw new ArgumentException("CreatedBy is required.");
            if (string.IsNullOrWhiteSpace(lastModifiedBy)) throw new ArgumentException("LastModifiedBy is required.");

            Id = id;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public static Customer Create(
            CustomerId id,
            string firstName,
            string lastName,
            string email,
            DateTime createdAt,
            DateTime lastModifiedAt,
            string createdBy,
            string lastModifiedBy)
        {
            return new Customer(
                id,
                firstName,
                lastName,
                email,
                createdAt,
                lastModifiedAt,
                createdBy,
                lastModifiedBy
            );
        }

        public override string ToString() => $"{FirstName} {LastName} ({Email})";
    }
}
