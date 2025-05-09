using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Data.Interceptors;

namespace OrderService.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var orderDbConnection = configuration.GetConnectionString("OrderDbConnection");

            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<OrderDbContext>((sp, options) =>
            {
                options.UseSqlServer(orderDbConnection)
                    .AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
            });

            return services;
        }
    }
}
