using System;
using System.Threading.Tasks;
using CustomerManagement.Search.Api;
using CustomerManagement.Search.Repositories.Impl;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming

namespace CustomerManagement.Search.Tests;

public class SearchByCompanyName
{
    ITestOutputHelper _output;

    public SearchByCompanyName(ITestOutputHelper output)
    {
        _output = output;
    }

    static async Task<(ServiceProvider, SqliteConnection)> Setup()
    {
        var cn = new SqliteConnection("Filename=:memory:");
        await cn.OpenAsync();
        var container = new ServiceCollection()
            .AddLogging()
            .AddCustomerSearch(options => options.UseSqlite(cn))
            .BuildServiceProvider();
        
        var db = container.GetRequiredService<CustomerSearchEfRepository>();
        await db.Database.EnsureCreatedAsync();
        
        return (container, cn);
    }

    static async Task Seed(CustomerSearchEfRepository db)
    {
    }
    static Task Seed(IServiceProvider container)
    {
        var db = container.GetRequiredService<CustomerSearchEfRepository>();
        return Seed(db);
    }
    
    [Fact(DisplayName = "When no matches are found, the list should be empty.")]
    public async Task ShouldReturnEmptyListWhenNotFound()
    {
        var (container, cn) = await Setup();

        ICustomerSearch search = container.GetRequiredService<ICustomerSearch>();
        CustomerSearchResponse results = await search.FindByCompanyNameAsync(new CustomerSearchByCompanyNameRequest() { CompanyName = "Acme"});
        var countReturned = results.PageSize;
        countReturned.Should().Be(0);
        await cn.DisposeAsync();
        await container.DisposeAsync();
    }
    
    [Fact(DisplayName = "When matches are found, the a paged list should be returned.")]
    public async Task ShouldReturnListOfResultsWhenFound()
    {
        var (container, cn) = await Setup();
        await Seed(container);
        
        ICustomerSearch search = container.GetRequiredService<ICustomerSearch>();
        CustomerSearchResponse results = await search.FindByCompanyNameAsync(new CustomerSearchByCompanyNameRequest() { CompanyName = "Acme"});
        var countReturned = results.PageSize;
        countReturned.Should().Be(2);
        await cn.DisposeAsync();
        await container.DisposeAsync();
    }
}
