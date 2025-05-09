
namespace OrderService.Infrastructure.Data.Seeding
{
    public static class CustomerSeedData
    {
        public static readonly CustomerId CustomerOne = CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        public static readonly CustomerId CustomerTwo = CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222"));

        public static IEnumerable<Customer> GetSeedCustomers()
        {
            yield return Customer.Create(
                id: CustomerOne,
                firstName: "John",
                lastName: "Wick",
                email: "john.Wick@example.com",
                createdAt: DateTime.UtcNow,
                lastModifiedAt: DateTime.UtcNow,
                createdBy: "seeder",
                lastModifiedBy: "seeder"
            );

            yield return Customer.Create(
                id: CustomerTwo,
                firstName: "Jose ",
                lastName: "Mourinho",
                email: "Mourinho@example.com",
                createdAt: DateTime.UtcNow,
                lastModifiedAt: DateTime.UtcNow,
                createdBy: "seeder",
                lastModifiedBy: "seeder"
            );
        }
    }
}
