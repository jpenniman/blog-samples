using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Rates.Api;
using Northwind.ShippingManagement.Rates.Repository;
using Northwind.ShippingManagement.Rates.Repository.Impl;

namespace Northwind.ShippingManagement.Rates;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShippingRates(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<RateDbContext>(options);
        services.AddScoped<IRateRepository, RateEfRepository>();
        services.AddScoped<IFreightCalculator, FreightCalculator>();

        return services;
    }
}