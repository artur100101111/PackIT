using Microsoft.Extensions.DependencyInjection;

namespace PackIT.Infrastructure.Context
{
    public static class Extensions
    {
        public static IHttpClientBuilder AddCorelation(this IHttpClientBuilder builder)
        {
            builder.AddHttpMessageHandler<CorrelationPropagationHandler>();
            return builder;
        } 
    }
}
