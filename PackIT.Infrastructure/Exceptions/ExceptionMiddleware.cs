using Microsoft.AspNetCore.Http;
using PackIt.Shared.Abstractions.Domain.Exceptions;
using PackIT.Infrastructure.Exceptions;

namespace PackIT.Shared.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private IEnumerable<IExceptionHandler> _handlers;

        public ExceptionMiddleware(IEnumerable<IExceptionHandler> handlers)
        {
            _handlers = handlers;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                foreach (var handler in _handlers)
                {
                    if (handler.CanHandle(ex))
                    {
                        await handler.HandleAsync(context, ex);
                        return;
                    }
                }
            }
        }
    }
}
