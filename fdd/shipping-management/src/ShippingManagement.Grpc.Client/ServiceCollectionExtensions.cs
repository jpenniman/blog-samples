using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Grpc.Sdk;
using ProtoBuf.Grpc.ClientFactory;

namespace grpc.client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFreightCalculatorClient(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new FreightCalculatorGrpcClientSettings();
        configuration.Bind("FreightCalculatorGrpcClient", config);
        services.AddSingleton(config);
        services.AddCodeFirstGrpcClient<IFreightCalculatorService>((provider, options) =>
        {
            var settings = provider.GetService<FreightCalculatorGrpcClientSettings>();
            options.Address = settings?.Server;
        });
        services.AddTransient<IFreightCalculator, FreightCalculatorGrpcClient>();
        return services;
    }
}