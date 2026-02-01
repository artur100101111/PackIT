using Microsoft.Extensions.DependencyInjection;
using PackIt.Shared.Abstractions.Queries;
using PackIt.Shared.Queries;
using System.Reflection;

namespace PackIT.Shared.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {

            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();

            // IQueryHandler implemented scan. Sculptor 
            var assembly = Assembly.GetCallingAssembly();
            services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
