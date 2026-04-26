using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using PackIt.Application;
using PackIt.Persistance;
using PackIt.Persistance.EF.Options;
using PackIT.Domain;
using PackIT.Infrastructure;
using PackIT.Infrastructure.Context;
using PackIT.Shared;
using PackIT.Shared.Infrastructure;
using Serilog;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Logging.ClearProviders();//REMOVE default providers
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext(); // REQUIRED
        });

        #region useSerilog instead of config json
        //builder.Host.UseSerilog((context, services, logConfig) =>
        //{
        //    logConfig
        //        // Minimum levels
        //        .MinimumLevel.Information()
        //        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        //        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        //        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)

        //        // Enrichers
        //        .Enrich.FromLogContext()// REQUIRED to have correlationId
        //        .Enrich.WithMachineName()

        //        // Sinks
        //        .WriteTo.Console()
        //        .WriteTo.File(
        //            path: "logs/app-.log",
        //            rollingInterval: RollingInterval.Day,
        //            retainedFileCountLimit: 7,
        //            outputTemplate:
        //             "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] ({CorrelationId}) [{MachineName}] {RequestPath} {Message:lj}{NewLine}{Exception}"
        //        );
    //});
        #endregion


        // Add services to the container
        builder.Services.AddControllers();
        //.AddJsonOptions(options =>
        //{
        //    options.JsonSerializerOptions.Converters.Add(
        //          new JsonStringEnumConverter());

        //}
        //);

        //Application Services

        //builder.Services.Configure<TimeOptions>(builder.Configuration.GetSection("LocalTime"));
        //builder.Services.Configure<LoggingOptions>(builder.Configuration.GetSection("Logging"));

        builder.Services.AddOptions<TimeOptions>().Bind(builder.Configuration.GetSection("LocalTime")).ValidateOnStart();
        builder.Services.AddSingleton<IValidateOptions<TimeOptions>, TimeOptionsValidator>();

        builder.Services.AddOptions<MsSQLOptions>().Bind(builder.Configuration.GetSection("MsSQL")).ValidateOnStart();
        builder.Services.AddSingleton<IValidateOptions<MsSQLOptions>, MsSqlOptionsValidator>();



        builder.Services.AddDomain();
        builder.Services.AddPersistance();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddSingleton(builder.Services);//IServiceCollection access for addShared->AddHostedService->GetRegisteredDbContext and run migration on it if needed.
        builder.Services.AddHttpContextAccessor();//used in Infrastructure CorrelationDelegatingHandler-IHttpContextAccessor is a framework service that gives access to the current HttpContextfrom code that is NOT part of the HTTP pipeline.
        builder.Services.AddShared();//hosted service with DbInitializer -> apply migration.


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddResponseCompression( options => { 
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();

        } );
        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });
        builder.Services.Configure<BrotliCompressionProviderOptions>(options => 
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });


        builder.Services.AddCors(o =>
        {
            o.AddPolicy("forntend"
                , p => p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                );
        }
         );

        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseResponseCompression();

        app.UseInfrastructure();//adds Infrastructure RequestIdCorrelation header for logging purposes

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}