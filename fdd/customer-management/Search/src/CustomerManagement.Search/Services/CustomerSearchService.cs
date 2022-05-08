using CustomerManagement.Search.Api;
using CustomerManagement.Search.Repositories;

namespace CustomerManagement.Search.Services;

class CustomerSearchService : ICustomerSearch
{
    readonly ICustomerSearchRepository _searchRepository;

    public CustomerSearchService(ICustomerSearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    public IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName)
    {
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException(
                "A partial company name must be provided. If you need to retrieve a paged list of all customers, use the GetAll() method instead.",
                nameof(companyName));
        
        return _searchRepository.FindByCompanyName(companyName);
    }
}
