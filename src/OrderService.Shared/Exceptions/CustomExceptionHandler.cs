using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OrderService.Shared.Exceptions
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private const string DomainException = "DomainException";
        private const string NotFoundException = "NotFoundException";

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            LogException(exception);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                ValidationException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ when IsNotFoundException(exception) => (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status404NotFound
               ),
                _ when IsDomainException(exception) => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
               )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }

        private void LogException(Exception exception)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    logger.LogWarning("Validation failed: {Message} | Errors: {@Errors}", validationException.Message, validationException.Errors);
                    break;

                default:
                    logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                    break;
            }
        }

        private static bool IsDomainException(Exception exception)
        {
            var type = exception.GetType();
            while (type != null)
            {
                if (type.Name == DomainException)
                    return true;
                type = type.BaseType;
            }
            return false;
        }

        private static bool IsNotFoundException(Exception exception)
        {
            var type = exception.GetType();
            while (type != null)
            {
                if (type.Name == NotFoundException)
                    return true;
                type = type.BaseType;
            }
            return false;
        }
    }
}