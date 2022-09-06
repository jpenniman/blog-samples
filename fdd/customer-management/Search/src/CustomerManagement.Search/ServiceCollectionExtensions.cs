using CustomerManagement.Search.Api;
using CustomerManagement.Search.Repositories;
using CustomerManagement.Search.Repositories.Impl;
using CustomerManagement.Search.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

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
    public static IServiceCollection AddCustomerSearch(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        //services.AddScoped<ICustomerSearch, CustomerSearchService>();
        services.AddCodeFirstGrpc();

        services.AddDbContext<ICustomerSearchRepository, CustomerSearchEfRepository>();
        
        return services;
    }
}
