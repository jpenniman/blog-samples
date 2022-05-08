using CustomerManagement.Search.Api;
using CustomerManagement.Search.Repositories;
using CustomerManagement.Search.Services;
using Microsoft.Extensions.DependencyInjection;

//using Northwind.CustomerManagement.Services.Impl;

namespace CustomerManagement.Search;

/// <summary>
/// IServiceCollection extensions for adding Customer Search functionality.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CustomerSearch functionality to the system.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomerSearch(this IServiceCollection services)
    {
        services.AddScoped<ICustomerSearch, CustomerSearchService>();

        return services;
    }
}
