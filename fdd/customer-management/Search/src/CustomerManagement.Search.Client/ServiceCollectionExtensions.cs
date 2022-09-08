using CustomerManagement.Search.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using ProtoBuf.Grpc.ClientFactory;

namespace CustomerManagement.Search.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerManagementSearchGrpcClient(this IServiceCollection services, IConfiguration configuration)
    {
        var customerSearchServiceClientSettings = new CustomerSearchClientSettings();
        configuration.Bind(CustomerSearchClientSettings.SECTION_NAME, customerSearchServiceClientSettings);
        services.AddSingleton(customerSearchServiceClientSettings);
        services.AddCodeFirstGrpcClient<ICustomerSearch>((provider, options) =>
            {
                var settings = provider.GetService<CustomerSearchClientSettings>();
                options.Address = settings?.Server;
            }).AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(300)))
            .AddTransientHttpErrorPolicy(policy =>
                policy.CircuitBreakerAsync(3, customerSearchServiceClientSettings.CircuitBreakerDuration));
        return services;
    }
}