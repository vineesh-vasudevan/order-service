using Microsoft.AspNetCore.Builder;

namespace OrderService.Infrastructure.Data.Seeding
{
    public static class DbSeederExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            var seeder = new DbSeeder(context);
            await seeder.SeedAsync();
        }
    }
}