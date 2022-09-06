using System.Runtime.Serialization;

namespace Northwind.ShippingManagement.Grpc.Sdk;

[DataContract]
public class CalculateResponse
{
    [DataMember(Order = 1)]
    public decimal Rate { get; set; }
}