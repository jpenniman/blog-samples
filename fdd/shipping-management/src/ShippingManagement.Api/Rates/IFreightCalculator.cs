using System.Threading.Tasks;

namespace Northwind.ShippingManagement.Api.Rates;

public interface IFreightCalculator
{
    internal const decimal DEFAULT_RATE = 26.08M;
    
    Task<decimal> Calculate(string country);
}