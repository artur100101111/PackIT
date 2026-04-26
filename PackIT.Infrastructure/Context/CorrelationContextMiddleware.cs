using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PackIT.Infrastructure.Context
{
    internal class CorrelationContextMiddleware : IMiddleware
    {
        private ILogger<CorrelationContextMiddleware> _logger;
        public const string _headerName = CorrelationConstants.HeaderName;
        public const string _itemKey = CorrelationConstants.ItemKey;

        public CorrelationContextMiddleware(ILogger<CorrelationContextMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = context.Request.Headers[_headerName].FirstOrDefault() ?? Guid.NewGuid().ToString();

            context.Items[_itemKey] = correlationId;
            context.Response.Headers[_headerName] = correlationId;

            CorrelationContext.CorrelationId = correlationId;

            try
            {
                using (_logger.BeginScope(new Dictionary<string, object>() { ["CorrelationId"] = correlationId, ["RequestPath"] = context.Request.Path }))
                {
                    await next(context);
                }
            }
            finally
            {
                CorrelationContext.CorrelationId = null;
            }
        }
    }
}
