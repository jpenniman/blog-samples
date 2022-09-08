using System.Runtime.Serialization;
using Northwind.Foundation;

namespace CustomerManagement.Search.Api;

[PublicAPI]
[DataContract]
public sealed class CustomerSearchResponse : PagedResponseBase<CustomerSearchResult>
{
    internal CustomerSearchResponse(IReadOnlyCollection<CustomerSearchResult> results)
    {
        Data = results;
    }

    internal CustomerSearchResponse(IReadOnlyCollection<Error> errors)
    {
        Errors = errors;
    }
}