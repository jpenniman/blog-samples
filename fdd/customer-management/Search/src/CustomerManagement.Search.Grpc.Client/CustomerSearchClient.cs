using CustomerManagement.Search.Api;
using CustomerManagement.Search.Grpc.Sdk;
using Grpc.Core;
using Polly;

namespace CustomerManagement.Search.Grpc.Client;

sealed class CustomerSearchClient : ICustomerSearch
{
    readonly ICustomerSearchService _customerSearchService;
    readonly ResiliencyPolicyAccessor _resiliency;
    
    public CustomerSearchClient(ICustomerSearchService customerSearchService, ResiliencyPolicyAccessor resiliency)
    {
        _customerSearchService = customerSearchService;
        _resiliency = resiliency;
    }

    public async IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName)
    {
        var request = new CustomerSearchByCompanyNameRequest { CompanyName = companyName };
        var response = await _resiliency.Policy.ExecuteAsync(() => _customerSearchService.FindByCompanyNameAsync(request));
        
        foreach (var result in response.Results)
            yield return result;
    }
}