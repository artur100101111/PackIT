using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PackIT.Shared.Services
{
    internal class AppInitializer : IHostedService
    {
        private IServiceProvider _serviceProvider;

        public AppInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        } 
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();


            //var dbContextTypess = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(a => a.GetTypes())
            //    .Where(a => typeof(DbContext).IsAssignableFrom(a) && !a.IsInterface && a != typeof(DbContext));

            ///
            var serviceDescriptor = scope.ServiceProvider.GetRequiredService<IServiceCollection>();

            var dbContextTypes = serviceDescriptor
                .Where(s => typeof(DbContext).IsAssignableFrom(s.ServiceType))
                .Select(s => s.ServiceType)
                .Distinct();


            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
                if (dbContext is null)
                    continue;
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
