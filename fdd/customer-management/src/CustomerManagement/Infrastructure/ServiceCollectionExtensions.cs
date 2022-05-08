using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.CustomerManagement.Api;
using Northwind.CustomerManagement.Api.DataMaintenance;
using Northwind.CustomerManagement.DataMaintenance.Repository;
using Northwind.CustomerManagement.DataMaintenance.Repository.Impl;
using Northwind.CustomerManagement.DataMaintenance.Services;
using Northwind.CustomerManagement.DataMaintenance.Services.Impl;
using Northwind.CustomerManagement.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Northwind.CustomerManagement;

/// <summary>
/// IServiceCollection extensions for adding customer management functionality
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the CustomerManagement component to the hosting container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbOptions">Entity Framework Context Options</param>
    /// <returns></returns>
    public static IServiceCollection AddCustomerManagement(this IServiceCollection services, Action<DbContextOptionsBuilder> dbOptions)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerRepository, CustomerEfRepository>();
        services.AddScoped<ICustomerUnitOfWork, CustomerEfUnitOfWork>();
        services.AddDbContext<CustomerDbContext>(dbOptions);
        services.AddSingleton<ICustomerEventStream, InProcessEventStream>();
        services.AddSingleton<ICustomerEventStreamPublisher>(p => (InProcessEventStream)p.GetService<ICustomerEventStream>()!);
        return services;
    }
}
