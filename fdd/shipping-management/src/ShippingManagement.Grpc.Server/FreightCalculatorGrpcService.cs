using System.Threading.Tasks;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Grpc.Sdk;

namespace Northwind.ShippingManagement.Grpc.Server;

public class FreightCalculatorGrpcService : IFreightCalculatorService
{
    readonly IFreightCalculator _calculator;

    public FreightCalculatorGrpcService(IFreightCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<CalculateResponse> Calculate(CalculateRequest calculateRequest)
    {
        return new CalculateResponse
        {
            Rate = await _calculator.Calculate(calculateRequest.Country)
        };

    }
}