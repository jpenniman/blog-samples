using CustomerManagement.Search.Api;
using CustomerManagement.Search.Grpc.Sdk;

namespace CustomerManagement.Search.Grpc.Server.Services;

class CustomerSearchService : ICustomerSearchService
{
    readonly ICustomerSearch _search;

    public CustomerSearchService(ICustomerSearch search)
    {
        _search = search;
    }

    public async Task<CustomerSearchResponse> FindByCompanyNameAsync(CustomerSearchByCompanyNameRequest request)
    {
        var resultList = new List<CustomerSearchResult>();
        var results = _search.FindByCompanyName(request.CompanyName);

        await foreach (var result in results)
        {
            resultList.Add(result);
        }

        return new CustomerSearchResponse { Results = resultList };
    }
}