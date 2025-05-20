using OrderService.Application.Common.Interfaces;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Data.Interceptors;
using OrderService.Infrastructure.Data.Repositories;
using OrderService.Infrastructure.Messaging;
using System.Reflection;

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
            services.AddScoped<IOrderEventPublisher, OrderEventPublisher>();

            services.AddMassTransit(x =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<BasketCheckoutConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = configuration["MessageBroker:Host"];
                    var username = configuration["MessageBroker:UserName"];
                    var password = configuration["MessageBroker:Password"];

                    cfg.Host(new Uri(host!), h =>
                    {
                        h.Username(username!);
                        h.Password(password!);
                    });

                    cfg.ReceiveEndpoint("basket-checkout-queue", e =>
                    {
                        e.ConfigureConsumer<BasketCheckoutConsumer>(context);
                    });
                });
            });

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