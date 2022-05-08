using CustomerManagement.Search.Api;
using CustomerManagement.Search.Grpc.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;

namespace CustomerManagement.Search.Grpc.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerManagementSearchGrpcClient(this IServiceCollection services, IConfiguration configuration)
    {
        var customerSearchServiceClientSettings = new CustomerSearchClientSettings();
        configuration.Bind(CustomerSearchClientSettings.SECTION_NAME, customerSearchServiceClientSettings);
        services.AddSingleton(customerSearchServiceClientSettings);
        services.AddSingleton<ResiliencyPolicyAccessor>();
        services.AddCodeFirstGrpcClient<ICustomerSearchService>((provider, options) =>
        {
            var settings = provider.GetService<CustomerSearchClientSettings>();
            options.Address = settings?.Server;
        });
        services.AddTransient<ICustomerSearch, CustomerSearchClient>();
        return services;
    }
}