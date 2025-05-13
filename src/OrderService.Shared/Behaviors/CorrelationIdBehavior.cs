using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OrderService.Shared.Behaviors
{
    public class CorrelationIdBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CorrelationIdBehavior<TRequest, TResponse>> _logger;

        public CorrelationIdBehavior(IHttpContextAccessor httpContextAccessor, ILogger<CorrelationIdBehavior<TRequest, TResponse>> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // Get or generate correlation ID
            var correlationId = httpContext?.Request.Headers.TryGetValue(CorrelationIdHeader, out var headerVal) == true
                ? headerVal.ToString()
                : Guid.NewGuid().ToString();

            // Optionally add to response headers
            httpContext?.Response.Headers.TryAdd(CorrelationIdHeader, correlationId);

            using (_logger.BeginScope("{CorrelationId}", correlationId))
            {
                _logger.LogInformation("Handling {RequestType} with CorrelationId: {CorrelationId}", typeof(TRequest).Name, correlationId);
                var response = await next();
                _logger.LogInformation("Handled {RequestType} with CorrelationId: {CorrelationId}", typeof(TRequest).Name, correlationId);
                return response;
            }
        }
    }
}