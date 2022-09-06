using Grpc.Net.Client;
using Northwind.ShippingManagement.Api;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Grpc.Sdk;
using Polly;
using ProtoBuf.Grpc.Client;

namespace grpc.client;

public class FreightCalculatorGrpcClient : IFreightCalculator
{
    static readonly AsyncPolicy<decimal> retryAsync = Policy<decimal>
        .Handle<Grpc.Core.RpcException>()
        .RetryAsync();
    
    readonly IFreightCalculatorService _client;

    public FreightCalculatorGrpcClient(IFreightCalculatorService client)
    {
        _client = client;
    }

    public async Task<decimal> Calculate(string country)
    {
        try
        {
            

            return await retryAsync.ExecuteAsync(async () =>
            {
                var response = await  _client.Calculate(new CalculateRequest { Country = country });
                return response.Rate;
            });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ApplicationException("Error communicating with service", e);
        }
    }
}