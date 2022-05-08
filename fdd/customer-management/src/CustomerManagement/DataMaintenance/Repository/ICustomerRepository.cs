using System.Threading.Tasks;
using Northwind.CustomerManagement.DataMaintenance.Domain;

namespace Northwind.CustomerManagement.DataMaintenance.Repository;

interface ICustomerRepository
{
    Task AddAsync(Customer customer);
}