using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Search.Api;
using CustomerManagement.Search.Grpc.Client;
using CustomerManagement.Search.Grpc.Sdk;
using CustomerManagement.Search.Grpc.Server;
using CustomerManagement.Search.Repositories.EntityFramework;
using FluentAssertions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Client;
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
            .AddCustomerSearch()
            .AddCustomerSearchEfRepository(options => options.UseSqlite(cn))
            .BuildServiceProvider();
        
        var db = container.GetRequiredService<CustomerSearchDbContext>();
        await db.Database.EnsureCreatedAsync();
        
        return (container, cn);
    }

    static async Task Seed(CustomerSearchDbContext db)
    {
        db.Customers.Add(new Customer("Acme Corporation"));
        db.Customers.Add(new Customer("Acme Limited"));
        db.Customers.Add(new Customer("Widgets R Us"));
        await db.SaveChangesAsync();
    }
    static Task Seed(IServiceProvider container)
    {
        var db = container.GetRequiredService<CustomerSearchDbContext>();
        return Seed(db);
    }
    
    [Fact(DisplayName = "When no matches are found, the list should be empty.")]
    public async Task ShouldReturnEmptyListWhenNotFound()
    {
        var (container, cn) = await Setup();

        ICustomerSearch search = container.GetRequiredService<ICustomerSearch>();
        IAsyncEnumerable<CustomerSearchResult> results = search.FindByCompanyName("Acme");
        var countReturned = await results.CountAsync();
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
        IAsyncEnumerable<CustomerSearchResult> results = search.FindByCompanyName("Acme");

        var countReturned = await results.CountAsync();
        countReturned.Should().Be(2);
        await cn.DisposeAsync();
        await container.DisposeAsync();
    }
}
