using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PackIt.Shared.Abstractions.Commands;

namespace PackIT.Shared.Commands
{
    public static class Extensions
    {
        /// <summary>
        /// Extension adds InMemoryCommandDispatcher, and scans calling assembly to register classes which implements ICommandHandler<>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();

            // ICommandHandler implemented scan.
            var assembly = Assembly.GetCallingAssembly();
            services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)), publicOnly:false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
