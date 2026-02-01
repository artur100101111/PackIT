using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PackIt.Application.Items;
using PackIt.Application.ItemTypes;
using PackIt.Application.Orders;
using PackIt.Application.Services;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Items;
using PackIt.Persistance.EF.ItemTypes;
using PackIt.Persistance.EF.Locations;
using PackIt.Persistance.EF.Options;
using PackIt.Persistance.EF.Orders;
using PackIt.Persistance.EF.Services;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Persistance.EF
{
    internal static class Extnsions
    {
        public static IServiceCollection AddMsSQL(this IServiceCollection services)
        { 
            //add repositories
            services.AddScoped<IOrderRepository, OrderWriteRepository>();
            services.AddScoped<IItemRepository, ItemWriteRepository>();
            services.AddScoped<IItemTypeRepository, ItemTypeWriteRepository>();
            services.AddScoped<ILocationRepository, LocationWriteRepository>();
            services.AddScoped<IOrderReadService, OrderReadService>();
            services.AddScoped<ILocationReadService, LocationReadService>();

            //add contexts
            services.AddDbContext<WriteDbContext>((sp, ctx) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<MsSQLOptions>>().Value;
                ctx.UseSqlServer(dbOptions.ConnectionString);

                var env = sp.GetRequiredService<IHostEnvironment>();
                if (env.IsDevelopment())
                {
                    ctx.LogTo(Console.WriteLine, LogLevel.Debug);
                     ctx.EnableSensitiveDataLogging();
                }
            }
            );
            services.AddDbContext<ReadDbContext>((sp, ctx) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<MsSQLOptions>>().Value;
                ctx.UseSqlServer(dbOptions.ConnectionString);

            });


            return services;
        }
    }
}
