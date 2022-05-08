using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Infrastructure;
using Northwind.ShippingManagement.Rates.Api;
using Northwind.ShippingManagement.Rates.Domain;
using Northwind.ShippingManagement.Rates.Repository.Impl;
using Xunit;
// ReSharper disable InconsistentNaming

namespace FreightTests;

public class When_calculating_freight
{
    [Fact]
    public async Task Given_a_country_then_return_a_fixed_rate()
    {
        var cn = new SqliteConnection("Filename=:memory:");
        cn.Open();
        var container = new ServiceCollection()
            .AddShippingManagement(options => options.UseSqlite(cn))
            .BuildServiceProvider();

        var db = container.GetService<RateDbContext>();
        await db.Database.EnsureCreatedAsync();
        db.ShippingRates.Add(new Rate("USA", 40.5400M));
        await db.SaveChangesAsync();
        var service = container.GetService<IFreightCalculator>();
        var rate = await service.Calculate("USA");
        rate.Should().Be(40.4500M);
    }
    
}