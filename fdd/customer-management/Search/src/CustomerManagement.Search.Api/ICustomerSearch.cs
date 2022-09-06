using System.ServiceModel;

namespace CustomerManagement.Search.Api;

/// <summary>
/// API that supports the search for customers in the system.
/// </summary>
[ServiceContract]
public interface ICustomerSearch
{
    /// <summary>
    /// Searches for a Customer whose CompanyName starts with the provided value.
    /// </summary>
    /// <param name="request">Search term. Can be full or partial company name.</param>
    /// <returns>A list of customer search results or an empty list if no customers match the search term.</returns>
    [OperationContract]
    Task<CustomerSearchResponse> FindByCompanyNameAsync(CustomerSearchByCompanyNameRequest request);
}
