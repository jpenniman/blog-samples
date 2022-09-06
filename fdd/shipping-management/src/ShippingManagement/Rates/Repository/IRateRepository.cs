using System.Threading.Tasks;
using Northwind.ShippingManagement.Rates.Domain;

namespace Northwind.ShippingManagement.Rates.Repository;

interface IRateRepository
{
    Task<Rate?> FindByCountry(string country);
}