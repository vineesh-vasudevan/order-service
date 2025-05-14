using Carter;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OrderService.Shared.Behaviors;
using OrderService.Shared.Exceptions;
using System.Text.Json.Serialization;

namespace OrderService.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var orderDbConnection = configuration.GetConnectionString("OrderDbConnection");
            services.AddCarter();

            services.AddHttpContextAccessor();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CorrelationIdBehavior<,>));
            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddHealthChecks()
                .AddSqlServer(orderDbConnection!);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            return app;
        }
    }
}