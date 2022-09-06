using CustomerManagement.Search.Api;
using HotChocolate;
using HotChocolate.Types;

namespace CustomerManagement.Search.GraphQL;

/// <summary>
/// GraphQL Query object
/// </summary>
[UsedImplicitly]
[ExtendObjectType(OperationTypeNames.Query)]
class SearchQuery
{
    /// <summary>
    /// Search for customers with a Company Name that begins with the provided search term.
    /// </summary>
    /// <param name="customerSearch">Injected.</param>
    /// <param name="companyName">Search term.</param>
    /// <returns>A list of matching search results, or an empty list if no Customers are found.</returns>
    public IAsyncEnumerable<CustomerSearchResult> FindByCompanyName([Service] ICustomerSearch customerSearch, string companyName)
    {
        return customerSearch.FindByCompanyName(companyName);
    }
}