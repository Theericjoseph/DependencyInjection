using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using TypesofDI.Configuration;
using TypesofDI.Enums;
using TypesofDI.Factories;
using TypesofDI.Processor;
using TypesOfDI;

namespace TypesofDI;

class Program
{
    static void Main(string[] args)
    {
        // 1) Build IConfiguration
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // 2) Setup DI container
        var services = new ServiceCollection()
            .AddSingleton<IConfiguration>(config)
            .AddLogging(builder => {
                builder.ClearProviders();
                builder.AddConsole();
                builder.AddDebug();
                builder.AddFilter<ConsoleLoggerProvider>(level => level >= LogLevel.Error);
                builder.AddFilter<DebugLoggerProvider>(level => level >= LogLevel.Information);
            })
            // bind PaymentConfig from JSON
            .Configure<Configuration.PaymentConfig>(
                config.GetSection("PaymentSettings"))
            .AddSingleton(sp =>
                sp.GetRequiredService<
                  IOptions<Configuration.PaymentConfig>>()
                  .Value)
            .AddSingleton(typeof(EnvironmentType), sp =>
                Enum.Parse<EnvironmentType>(
                    sp.GetRequiredService<IConfiguration>()["Environment"]))
            .AddSingleton<Factories.PaymentProcessorFactory>()
            .AddTransient<App>()   // our entry-point wrapper
            // done
            .BuildServiceProvider();

        // 3) Resolve and run
        var app = services.GetRequiredService<App>();
        app.Run();
    }
    
}
