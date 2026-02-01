using IdGen;
using Microsoft.Extensions.DependencyInjection;
using PackIt.Application.Services;
using PackIt.Persistance.EF;
using PackIt.Persistance.EF.Orders.Mapper;
using PackIt.Persistance.EF.Services;
using PackIt.Persistance.EF.Shared;
using PackIt.Persistance.Services;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Shared;
using PackIT.Shared.Queries;

namespace PackIt.Persistance
{
    public static class Extensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddMsSQL();// from Persistance-> EF-> Options -not IOptions<> so avoiding program.cs configuration.
            services.AddQueries();// from Shared

            //for manual options configuration.
            //var timeOptions = configuration.GetOptions<TimeOptions>("LocalTime");

            services.AddSingleton<IDateTimeService, DateTimeService>(); // from Persistance
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //from persistance
       
            services.AddAutoMapper(cfg=>cfg.AddMaps(typeof(OrderMappingProfile).Assembly));
            services.AddSingleton<IdGenerator>(sp =>
            {
                var epoch = new DateTime(2025, 1, 1);

                #region IdConfig
                //var idConfig = new IdStructure(
                //                timestampBits: 41,
                //                generatorIdBits: 10, // 
                //                sequenceBits: 12
                //            );
                #endregion  
                var options = new IdGeneratorOptions
                {
                    TimeSource = new DefaultTimeSource(epoch),
                    SequenceOverflowStrategy = SequenceOverflowStrategy.SpinWait,
                   //  IdStructure = idConfig
                };

            return new IdGenerator(1, options);
            });
            services.AddSingleton<ISnowflakeIdGenerator, SnowflakeIdGenerator>();


            return services;
        }
    }
}
