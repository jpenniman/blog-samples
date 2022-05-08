using System.Runtime.Serialization;

namespace Northwind.ShippingManagement.Grpc.Sdk;

[DataContract]
public class CalculateRequest
{
    [DataMember(Order = 1)]
    public string Country { get; set; } = string.Empty;
}