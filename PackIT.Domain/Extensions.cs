using Microsoft.Extensions.DependencyInjection;
using PackIT.Domain.Items.Factories;
using PackIT.Domain.Locations.Factories;
using PackIT.Domain.Orders.Factories;
using PackIT.Domain.Orders.Factory;

namespace PackIT.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<ILocationFactory, LocationFactory>();
            services.AddScoped<IItemFactory,ItemFactory>();

            return services;
        }
    }
}
