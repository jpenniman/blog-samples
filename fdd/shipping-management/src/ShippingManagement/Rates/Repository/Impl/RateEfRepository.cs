using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Northwind.ShippingManagement.Infrastructure;
using Northwind.ShippingManagement.Rates.Domain;

namespace Northwind.ShippingManagement.Rates.Repository.Impl;

sealed class RateEfRepository : IRateRepository
{
    readonly RateDbContext _dbContext;

    public RateEfRepository(RateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Rate?> FindByCountry(string country)
    {
        ArgumentNullException.ThrowIfNull(country, nameof(country));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be blank. Please provide a valid country code.", country);
        
        return await _dbContext.ShippingRates
            .TagWithCallSite()
            .TagWith($"{nameof(IRateRepository)}.{nameof(FindByCountry)}")
            .Where(r => r.Country == country)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }
    

}