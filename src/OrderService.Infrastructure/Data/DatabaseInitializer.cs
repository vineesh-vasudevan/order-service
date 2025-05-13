using Microsoft.AspNetCore.Builder;

namespace OrderService.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}