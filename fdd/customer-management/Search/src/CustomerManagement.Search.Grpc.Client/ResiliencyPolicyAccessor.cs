using CustomerManagement.Search.Grpc.Sdk;
using Grpc.Core;
using Polly;

namespace CustomerManagement.Search.Grpc.Client;

sealed class ResiliencyPolicyAccessor
{
    public ResiliencyPolicyAccessor(CustomerSearchClientSettings settings)
    {
        AsyncPolicy<CustomerSearchResponse> retryAsync = Policy<CustomerSearchResponse>
            .Handle<RpcException>()
            .RetryAsync();
    
        AsyncPolicy<CustomerSearchResponse> circuitBreakerAsync = Policy<CustomerSearchResponse>
            .Handle<RpcException>()
            .CircuitBreakerAsync(3, settings.CircuitBreakerDuration);

        Policy = circuitBreakerAsync
            .WrapAsync(retryAsync);
    }

    public AsyncPolicy<CustomerSearchResponse> Policy { get; }
}