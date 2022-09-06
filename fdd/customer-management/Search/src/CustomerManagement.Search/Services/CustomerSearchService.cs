using CustomerManagement.Search.Api;
using CustomerManagement.Search.Repositories;
using Northwind.Foundation;

namespace CustomerManagement.Search.Services;

class CustomerSearchService : ICustomerSearch
{
    readonly ICustomerSearchRepository _searchRepository;

    public CustomerSearchService(ICustomerSearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    public async Task<CustomerSearchResponse> FindByCompanyNameAsync(CustomerSearchByCompanyNameRequest request)
    {
        if (!request.IsValid())
            return new CustomerSearchResponse(request.Validate());

        try
        {
            var results = await _searchRepository.FindByCompanyName(request.CompanyName).ToArrayAsync();
            return new CustomerSearchResponse(results);
        }
        catch (Exception ex)
        {
            return new CustomerSearchResponse(new[] { new Error("FIND_ERROR", ex.Message) });
        }
    }
}
