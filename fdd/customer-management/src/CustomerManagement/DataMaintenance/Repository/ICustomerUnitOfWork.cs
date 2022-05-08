using Northwind.Foundation.Repositories;

namespace Northwind.CustomerManagement.DataMaintenance.Repository;

interface ICustomerUnitOfWork : IUnitOfWork
{
    ICustomerRepository Customers { get; }
}