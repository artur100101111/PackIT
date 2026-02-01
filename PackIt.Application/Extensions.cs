using Microsoft.Extensions.DependencyInjection;
using PackIt.Application.Items.Factories;
using PackIt.Application.Locations;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Locations.Factories;
using PackIt.Application.Orders.Factories;
using PackIT.Domain.Orders.States;
using PackIT.Shared.Commands;
using PackIT.Shared.DtoTree.DtoTreeBuilder;

namespace PackIt.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<LocationApplicationFactory>();
            services.AddScoped<IOrderApplicationFactroy,OrderApplicationFactroy>();
            services.AddScoped<ItemApplicationFactory>();
            services.AddScoped<IOrderItemsFactory, OrderItemsFactory>();

            services.AddSingleton<IOrderStateService, OrderStateService>();
            services.AddCommands(); // extension from shared.
            services.AddTransient<IDtoTreeBuilder<LocationDto>, LocationDtoTreeBuilder>();

            return services;
        }
    }
}
