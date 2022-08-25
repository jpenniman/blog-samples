using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var logging = new Logging();
config.Bind("Logging", logging);
LogEventLevel GetLogEventLevel(LogLevel logLevel)
{
    return (LogEventLevel)logLevel;
}

var loglevelsCache = new Dictionary<string, LoggingLevelSwitch>();
loglevelsCache["Default"] = new LoggingLevelSwitch(GetLogEventLevel(logging.LogLevel["Default"]));

var loggerConfig = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(config, "Logging:Serilog")
    .MinimumLevel.ControlledBy(loglevelsCache["Default"]);

foreach (var levelOverride in logging.LogLevel.Where(kv => kv.Key != "Default"))
{
    loglevelsCache[levelOverride.Key] = new LoggingLevelSwitch(GetLogEventLevel(levelOverride.Value));
    loggerConfig.MinimumLevel.Override(levelOverride.Key, loglevelsCache[levelOverride.Key]);
}

Log.Logger = loggerConfig.CreateLogger();

var container = new ServiceCollection()
    .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(Log.Logger))
    .AddOptions()
    .Configure<Logging>(config.GetSection("Logging"))
    .Configure<Foo>(config.GetSection("Foo"))
    .BuildServiceProvider();

//var options = container.GetRequiredService<IOptions<Foo>>();
var options = container.GetRequiredService<IOptionsMonitor<Foo>>();
var loggingOptions = container.GetRequiredService<IOptionsMonitor<Logging>>();
loggingOptions.OnChange((loggingConfig, s) =>
{
    foreach (var logLevel in loggingConfig.LogLevel)
    {
        loglevelsCache[logLevel.Key].MinimumLevel = GetLogEventLevel(logLevel.Value);
    }
});

var logger = container.GetRequiredService<ILogger<Program>>();
while (true)
{
    logger.LogDebug("test");
    //var snapshot = container.CreateScope().ServiceProvider.GetRequiredService<IOptionsSnapshot<Foo>>();
    //Console.WriteLine(config["Foo:Bar"]);
    Console.WriteLine(options.CurrentValue.Bar);
    //Console.WriteLine(snapshot.Value.Bar);
    Thread.Sleep(1000);
}


class Foo
{
    public int Bar { get; set; }
}

class Logging
{
    public Dictionary<string, LogLevel> LogLevel { get; set; }
}

static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddScoped<Foo>(container =>
        {
            var settings = new Foo();
            var configuration = container.GetRequiredService<IConfigurationRoot>();
            configuration.Bind("Foo", settings);
            return settings;
        });
        return services;
    }
}