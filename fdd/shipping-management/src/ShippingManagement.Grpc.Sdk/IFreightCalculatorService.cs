using System.ServiceModel;

namespace Northwind.ShippingManagement.Grpc.Sdk;

[ServiceContract(Name = "Northwind.ShippingManagement.FreightCalculatorService")]
public interface IFreightCalculatorService
{
    [OperationContract]
    Task<CalculateResponse> Calculate(CalculateRequest calculateRequest);
}