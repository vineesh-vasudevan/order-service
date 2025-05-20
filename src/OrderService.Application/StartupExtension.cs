using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Shared.Behaviors;

namespace OrderService.Application
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(CreateOrderCommand).Assembly;
            services.AddValidatorsFromAssembly(assembly);
            services.AddAutoMapper(typeof(OrderItemProfile).Assembly);
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddFeatureManagement();
            services.AddScoped<ÌCreateOrderCommandFactory, CreateOrderCommandFactory>();
            return services;
        }
    }
}