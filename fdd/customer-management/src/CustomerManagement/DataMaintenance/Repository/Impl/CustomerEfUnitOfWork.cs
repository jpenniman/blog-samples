using System.Threading.Tasks;

namespace Northwind.CustomerManagement.DataMaintenance.Repository.Impl;

class CustomerEfUnitOfWork : ICustomerUnitOfWork
{
    readonly CustomerDbContext _db;

    public CustomerEfUnitOfWork(CustomerDbContext db, ICustomerRepository customers)
    {
        _db = db;
        Customers = customers;
    }

    public ICustomerRepository Customers { get; }
    
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}