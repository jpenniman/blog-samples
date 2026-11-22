using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Search.Api;
using CustomerManagement.Search.Client;
using CustomerManagement.Search.Repositories.Impl;
using FluentAssertions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Client;
using Xunit;
using Xunit.Abstractions;

namespace CustomerManagement.Search.Tests;

public class GrpcContractTests : IDisposable
{
    ITestOutputHelper _output;
    readonly TestServer _server;
    
    public GrpcContractTests(ITestOutputHelper output)
    {
        _output = output;
        _server = InitializeTestServer();
    }
    
    public void Dispose()
    {
        _server.Dispose();
    }
    
    static void Seed(CustomerSearchEfRepository db)
    {
    }

    static TestServer InitializeTestServer()
    {
        // Have to create a single, peristant connection to share the same in-memory database between the seed and tests.
        var cn = new SqliteConnection("Filename=:memory:");
        cn.Open();
        
        // We want a custom host for testing, not the microservice wrapped up in a WebApplicationFactory<T>.
        var host = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer(); // Test Server instead of Kestrel
                webHost.ConfigureServices(services =>
                {
                    services.AddLogging();
                    services.AddCustomerSearch(options => options.UseSqlite(cn));
                });
                webHost.Configure(app =>
                {
                    app.UseRouting();
                    app.UseCustomerManagementSearchGrpcServices();
                });
            })
            .Build();
        host.Start();

        var db = host.Services.GetRequiredService<CustomerSearchEfRepository>();
        db.Database.EnsureCreated();
        Seed(db);
        
        return host.GetTestServer();
    }

    IServiceProvider Setup()
    {
        GrpcClientFactory.AllowUnencryptedHttp2 = true;
        var channel = GrpcChannel.ForAddress(_server.BaseAddress, new GrpcChannelOptions()
        {
            HttpClient = _server.CreateClient()
        });

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { $"{CustomerSearchClientSettings.SECTION_NAME}:Server", _server.BaseAddress.ToString()}
            })
            .Build();
        
        return new ServiceCollection()
            .AddSingleton(config)
            .AddCustomerManagementSearchClient(config)
            .Replace(ServiceDescriptor.Transient(_ => channel.CreateGrpcService<ICustomerSearch>()))
            .BuildServiceProvider();
    }
    
    
    [Fact]
    public async Task FindByCustomerName()
    {
        var container = Setup();
        var client = container.GetRequiredService<ICustomerSearch>();
        CustomerSearchResponse results = await client.FindByCompanyNameAsync(new CustomerSearchByCompanyNameRequest() { CompanyName = "Acme"});

        // var allResults = results.Data.ToArray();
        // allResults.Length.Should().Be(2);
        // allResults[0].Id.Should().BeGreaterThan(0);
        // allResults[0].CompanyName.Should().NotBeNullOrWhiteSpace();
        // allResults[0].CompanyName.Should().BeOfType<string>();
        // var type = allResults[0].GetType();
        // type.GetProperty("Phone").Should().Return<string>();
        // type.GetProperty("PostalCode").Should().Return<string>();
    }
}