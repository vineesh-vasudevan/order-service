using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Data.Interceptors;
using OrderService.Infrastructure.Data.Repositories;

namespace OrderService.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var orderDbConnection = configuration.GetConnectionString("OrderDbConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DomainEventsInterceptor>();
            services.AddScoped<IUnitOfWork, OrderUnitOfWork>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddDbContext<OrderDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(orderDbConnection);
            });

            return services;
        }
    }
}