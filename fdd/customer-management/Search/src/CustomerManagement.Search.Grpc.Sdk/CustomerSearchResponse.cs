using System.Collections;
using System.Runtime.Serialization;
using CustomerManagement.Search.Api;

namespace CustomerManagement.Search.Grpc.Sdk;

[PublicAPI]
[DataContract]
public class CustomerSearchResponse
{
    [DataMember(Order = 1)]
    public ICollection<CustomerSearchResult> Results { get; [UsedImplicitly] set; }
}