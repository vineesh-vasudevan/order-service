using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Application
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
