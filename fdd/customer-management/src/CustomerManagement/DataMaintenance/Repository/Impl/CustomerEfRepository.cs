using System.Threading.Tasks;
using Northwind.CustomerManagement.DataMaintenance.Domain;

namespace Northwind.CustomerManagement.DataMaintenance.Repository.Impl;

class CustomerEfRepository : ICustomerRepository
{
    readonly CustomerDbContext _db;

    public CustomerEfRepository(CustomerDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Customer customer)
    {
        await _db.Customers.AddAsync(customer);
    }
}