using Microsoft.EntityFrameworkCore.Design;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "OrderService.Api");

            var configuration = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .Build();

            var dbConnection = configuration.GetConnectionString("OrderDbConnection");
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionsBuilder.UseSqlServer(dbConnection);

            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}