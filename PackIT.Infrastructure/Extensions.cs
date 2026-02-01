using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Infrastructure.Context;
using PackIT.Infrastructure.Logging;
using PackIT.Shared.Exceptions;

namespace PackIT.Infrastructure
{
    public static class Extensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<CorrelationContextMiddleware>();
            services.AddScoped<CorrelationPropagationHandler>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddLoggingSrvices();


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
