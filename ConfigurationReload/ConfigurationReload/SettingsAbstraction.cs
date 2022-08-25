using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConfigurationReload;

public interface ISettings<T> where T : class
{
    event EventHandler<T> OnChange;
    T Value { get; }
}

sealed class ConfigurationOptionsSettings<T> : ISettings<T> where T : class
{
    readonly IOptionsMonitor<T> _optionsMonitor;

    public ConfigurationOptionsSettings(IOptionsMonitor<T> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
        _optionsMonitor.OnChange(options => OnChange?.Invoke(this, options));
    }

    public event EventHandler<T>? OnChange;
    public T Value => _optionsMonitor.CurrentValue;
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services, IConfiguration configuration) where T : class
    {
        services.Configure<T>(configuration);
        services.AddSingleton<ISettings<T>, ConfigurationOptionsSettings<T>>();
        return services;
    }
}