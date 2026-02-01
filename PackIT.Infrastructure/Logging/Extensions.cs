using Microsoft.Extensions.DependencyInjection;
using PackIt.Shared.Abstractions.Commands;

namespace PackIT.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IServiceCollection AddLoggingSrvices(this IServiceCollection services)
        {
            {
                services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
                return services;
            }
        }
    }
}
