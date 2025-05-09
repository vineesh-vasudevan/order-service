using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var orderDbConnection = configuration.GetConnectionString("OrderDbConnection");

            services.AddDbContext<OrderDbContext>((sp, options) =>
            {
                options.UseSqlServer(orderDbConnection);
            });

            return services;
        }
    }
}
