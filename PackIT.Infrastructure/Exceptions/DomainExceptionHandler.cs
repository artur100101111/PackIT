using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PackIt.Shared.Abstractions.Domain.Exceptions;
using System.Text.Json;

namespace PackIT.Infrastructure.Exceptions
{
    internal class DomainExceptionHandler : IExceptionHandler
    {
        private ILogger<DomainExceptionHandler> _logger;

        public bool CanHandle(Exception ex) => ex is PackItException;
        public DomainExceptionHandler(ILogger<DomainExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(HttpContext context, Exception ex)
        {
            var domainException = (PackItException)ex;
            string? correlationId = context.Items.TryGetValue("correlationId", out var value) ? value.ToString() : null;
            var errorCode = ToUnderscoreCase(domainException.GetType().Name.Replace("Exception", string.Empty));

            var status = domainException switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                AlreadyExistsException => StatusCodes.Status409Conflict,
                DomainRuleViolationException => StatusCodes.Status409Conflict,
                PackItException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };


            var problem = CreateProblemDetails(context, status,errorCode, domainException.Message, correlationId, domainException);

            var json = JsonSerializer.Serialize(problem);

            _logger.LogWarning(domainException, "Domain error occured. Error Code: {ErrorCode} Status: {Status}, CorrelationId: {CorrelationId} Path: {Path}",
               errorCode,
               status,
               correlationId,
               context.Request.Path);

            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(json);
        }

        private CustomProblemDetails CreateProblemDetails(HttpContext context, int status,string errorCode, string message, string? correlationId, PackItException domainException)
        { 

            var problem = new CustomProblemDetails
            { 
                Status = status,
                Title = "Domain Error",
                Detail = message,
                Instance = context.Request.Path
            };
            problem.Extensions["errorCode"] = errorCode;
            problem.Extensions["correlationId"] = correlationId!;

            return problem;
        }

        private string ToUnderscoreCase(string input)
        {
            return string.Concat((input ?? string.Empty)
                       .Select(
                       (x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(input![i - 1])
                       ? $"_{x}" : x.ToString()))
                       .ToLowerInvariant();


            //if (string.IsNullOrEmpty(input))
            //    return input;
            //var result = new System.Text.StringBuilder();
            //for (int i = 0; i < input.Length; i++)
            //{
            //    if (char.IsUpper(input[i]) && i > 0)
            //        result.Append('_');
            //    result.Append(char.ToLowerInvariant(input[i]));
            //}
            //return result.ToString();
        }
    }
}
