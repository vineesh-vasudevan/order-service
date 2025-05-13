namespace OrderService.Infrastructure.Data.Seeding
{
    public static class CustomerSeedData
    {
        public static readonly CustomerId CustomerOne = CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        public static readonly CustomerId CustomerTwo = CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222"));

        public static IEnumerable<Customer> GetSeedCustomers()
        {
            var customer1 = Customer.Create(
                id: CustomerOne,
                firstName: "John",
                lastName: "Wick",
                email: "john.Wick@example.com"
            );

            customer1.SetAudit("System", true);

            yield return customer1;

            var customer2 = Customer.Create(
                id: CustomerTwo,
                firstName: "Jose",
                lastName: "Mourinho",
                email: "Mourinho@example.com"
            );

            customer2.SetAudit("System", true);

            yield return customer2;
        }
    }
}