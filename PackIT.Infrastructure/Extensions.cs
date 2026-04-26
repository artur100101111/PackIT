using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Infrastructure.Context;
using PackIT.Infrastructure.Exceptions;
using PackIT.Infrastructure.Logging;
using PackIT.Shared.Exceptions;

namespace PackIT.Infrastructure
{
    public static class Extensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<CorrelationContextMiddleware>();
            services.AddTransient<CorrelationPropagationHandler>();
            services.AddTransient<ExceptionMiddleware>();

            services.AddTransient<IExceptionHandler, DomainExceptionHandler>();
            services.AddTransient<IExceptionHandler, DefaultExceptionHandler>();

            services.AddLoggingSrvices();


            services.AddHttpClient("ExternalApi")//adds HTTP Client with its own configuration and  pipeline - it can be used  by name-specific outgoing HTTP client configuration.
                .AddCorelation(); //extansion to add CorrelationPropagationHandler to the pipeline of ongoing Http requests made by this client.

            return services;
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationContextMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();




            return app;
        }

    }
}
