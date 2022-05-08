namespace CustomerManagement.Search.Api;

/// <summary>
/// API that supports the search for customers in the system.
/// </summary>
public interface ICustomerSearch
{
    /// <summary>
    /// Searches for a Customer whose CompanyName starts with the provided value.
    /// </summary>
    /// <param name="companyName">Search term. Can be full or partial company name.</param>
    /// <returns>A list of customer search results or an empty list if no customers match the search term.</returns>
    IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName);
}
