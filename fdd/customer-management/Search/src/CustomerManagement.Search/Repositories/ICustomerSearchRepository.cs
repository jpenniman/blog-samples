using CustomerManagement.Search.Api;

namespace CustomerManagement.Search.Repositories;

interface ICustomerSearchRepository
{
    IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName);
}