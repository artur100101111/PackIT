using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PackIT.Infrastructure.Exceptions;
using System.Text.Json;

namespace PackIT.Shared.Exceptions
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        private ILogger<DefaultExceptionHandler> _logger;

        public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
        {
            _logger = logger;
        }
        public bool CanHandle(Exception ex) => true;

        public async Task HandleAsync(HttpContext context, Exception ex)
        {
            string? correlationId = context.Items.TryGetValue("correlationId", out var value) ? value.ToString() : null;

            var problem = CreateProblemDetails(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.", correlationId);
            var method = context.Request.Method;    
            _logger.LogError(ex, 
                "Unhandled Exception. Metod {Method}, Path: {Path}, CorrelationId: {CorrelaltionId}",
                method,
                context.Request.Path,
                correlationId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }


        private CustomProblemDetails CreateProblemDetails(HttpContext context, int status, string message, string? correlationId)
        {
            var problem = new CustomProblemDetails
            {
                Status = status,
                Title = "Internal Server Error",
                Detail = message,
                Instance = context.Request.Path,
            };
            problem.Extensions["errorCode"] = "internal_serwer_error";
            problem.Extensions["correlationId"] = correlationId!;

            return problem;
        }
    }
}
