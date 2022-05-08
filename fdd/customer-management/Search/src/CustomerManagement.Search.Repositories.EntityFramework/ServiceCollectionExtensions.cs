using CustomerManagement.Search.Repositories;
using CustomerManagement.Search.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace CustomerManagement.Search;

/// <summary>
/// IServiceCollection extensions for adding Customer Search functionality.
/// </summary>
[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CustomerSearch functionality to the system.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomerSearchEfRepository(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<CustomerSearchDbContext>(options);
        services.AddScoped<ICustomerSearchRepository, CustomerSearchEfRepository>();

        return services;
    }
}
