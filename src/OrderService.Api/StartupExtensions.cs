using Carter;
using MediatR;
using OrderService.Shared.Behaviors;
using OrderService.Shared.Exceptions;
using System.Text.Json.Serialization;

namespace OrderService.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();

            services.AddHttpContextAccessor();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CorrelationIdBehavior<,>));
            services.AddExceptionHandler<CustomExceptionHandler>();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            return app;
        }
    }
}