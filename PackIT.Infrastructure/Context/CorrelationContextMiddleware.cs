using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PackIT.Infrastructure.Context
{
    internal class CorrelationContextMiddleware : IMiddleware
    {
        private ILogger<CorrelationContextMiddleware> _logger;
        public const string _headerName = "X-Correlation-Id";

        public CorrelationContextMiddleware(ILogger<CorrelationContextMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = context.Request.Headers[_headerName].FirstOrDefault() ?? Guid.NewGuid().ToString() ;

            context.Items["correlationId"] = correlationId ;
            context.Response.Headers[_headerName] = correlationId;

            using (_logger.BeginScope(new Dictionary<string, object>() { ["CorrelationId"] = correlationId, ["RequestPath"] = context.Request.Path }))
            {
                await next(context);
            }
        }
    }
}
