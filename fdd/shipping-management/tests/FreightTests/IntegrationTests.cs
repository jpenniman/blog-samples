using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using grpc.client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Xunit;

namespace FreightTests;

public class IntegrationTests
{
    [Fact]
    public async Task TestGrpc()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "FreightCalculatorGrpcClient:Server", "https://localhost:7218" }
            })
            .Build();
        
        var container = new ServiceCollection()
            .AddFreightCalculatorClient(configuration)
            .BuildServiceProvider();

        var client = container.GetService<IFreightCalculator>();
        var rate = await client.Calculate("USA");
        Assert.Equal(40.54M, rate);
    }
}