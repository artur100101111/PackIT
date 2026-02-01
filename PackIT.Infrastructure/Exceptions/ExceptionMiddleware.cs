using Microsoft.AspNetCore.Http;
using PackIT.Domain.Common;
using System.Text.Json;

namespace PackIT.Shared.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (PackItException ex)
            {
                string? correlationId = context.Items.TryGetValue("correlationId", out var value) ? value.ToString() : null;

                context.Response.StatusCode = 400;
                context.Response.Headers.Add("content-type", "application/json");
                var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));
                var json = JsonSerializer.Serialize(new { ErrorCode= errorCode, Message= ex.Message, CorrelationId= correlationId });
                await context.Response.WriteAsync(json);
                return;
            }
        }

        private object ToUnderscoreCase(string value)
        {
            return string.Concat((value??string.Empty)
                         .Select(
                         (x,i) => i> 0 && char.IsUpper(x) && !char.IsUpper(value![i-1]) 
                         ? $"_{x}" : x.ToString()))
                         .ToLowerInvariant();
        }
    }
}
