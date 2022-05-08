using System.Runtime.Serialization;

namespace CustomerManagement.Search.Grpc.Sdk;

[DataContract]
public class CustomerSearchByCompanyNameRequest
{
    [DataMember(Order = 1)] public string CompanyName { get; set; } = string.Empty;
}