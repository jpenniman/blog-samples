using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Rates;
using Northwind.ShippingManagement.Rates.Api;
using Northwind.ShippingManagement.Rates.Repository;
using Northwind.ShippingManagement.Rates.Repository.Impl;

namespace Northwind.ShippingManagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShippingManagement(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddShippingRates(options);

        return services;
    }
}