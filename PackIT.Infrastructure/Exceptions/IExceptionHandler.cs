using Microsoft.AspNetCore.Http;

namespace PackIT.Infrastructure.Exceptions
{
    public interface IExceptionHandler
    {
        public bool CanHandle(Exception ex);
        Task HandleAsync(HttpContext context, Exception ex);
    }
}
