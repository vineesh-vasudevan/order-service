namespace OrderService.Infrastructure.Data.Seeding
{
    public class DbSeeder(OrderDbContext context)
    {
        public async Task SeedAsync()
        {
            await SeedCustomersAsync();
            await SeedProductsAsync();
            await SeedOrdersWithItemsAsync();
        }

        private async Task SeedCustomersAsync()
        {
            if (!context.Customers.Any())
            {
                var customers = CustomerSeedData.GetSeedCustomers();
                context.Customers.AddRange(customers);
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedProductsAsync()
        {
            if (!context.Products.Any())
            {
                var products = ProductSeedData.GetSeedProducts();
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedOrdersWithItemsAsync()
        {
            if (!context.Orders.Any())
            {
                var orders = OrderSeedData.GetSeedOrders();
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }
        }
    }
}