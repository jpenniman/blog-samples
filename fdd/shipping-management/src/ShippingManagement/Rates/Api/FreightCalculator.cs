using System.Threading.Tasks;
using Northwind.ShippingManagement.Api.Rates;
using Northwind.ShippingManagement.Rates.Repository;

namespace Northwind.ShippingManagement.Rates.Api;

class FreightCalculator : IFreightCalculator
{
    readonly IRateRepository _rateRepository;

    public FreightCalculator(IRateRepository rateRepository)
    {
        _rateRepository = rateRepository;
    }

    public async Task<decimal> Calculate(string country)
    {
        var rate = await _rateRepository.FindByCountry(country);

        return rate?.FlatRate ?? IFreightCalculator.DEFAULT_RATE;
    }
}