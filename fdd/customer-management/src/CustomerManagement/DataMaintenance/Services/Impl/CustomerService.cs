using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.CustomerManagement.Api;
using Northwind.CustomerManagement.Api.DataMaintenance;
using Northwind.CustomerManagement.DataMaintenance.Domain;
using Northwind.CustomerManagement.DataMaintenance.Repository.Impl;
using Customer = Northwind.CustomerManagement.Api.DataMaintenance.Customer;

namespace Northwind.CustomerManagement.DataMaintenance.Services.Impl;

class CustomerService : ICustomerService
{
    readonly CustomerDbContext _dbContext;
    readonly ICustomerEventStreamPublisher _eventStream;

    public CustomerService(CustomerDbContext dbContext, ICustomerEventStreamPublisher eventStream)
    {
        _dbContext = dbContext;
        _eventStream = eventStream;
    }

    public Task DeleteCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetAsync(long id)
    {
        var entity = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
        if (entity == null)
            return null;
        
        var model = Customer.Create(entity.CompanyName);
        model.Id = entity.Id;
        model.Phone = entity.PhoneNumber;
        model.Version = entity.Version;

        return model;
    }

    public async Task CreateNewCustomerAsync(Customer customer)
    {
        bool isNew = customer.Id == 0;

        Domain.Customer? entity;
        if (isNew)
        {
            entity = await _dbContext.Customers.FindAsync(customer.Id);
            if (entity == null)
                throw new InvalidOperationException();
            if (entity.Version != customer.Version)
                throw new DbUpdateConcurrencyException();
        }
        else
        {
            entity = new Domain.Customer(customer.CompanyName);
            await _dbContext.AddAsync(entity);
        }

        entity.PhoneNumber = customer.Phone;

        var errors = entity.Validate();
        if (errors.Any())
            throw new ValidationException(errors);
        
        customer.Version++;
        await _dbContext.SaveChangesAsync();
        await _dbContext.SaveChangesAsync();

        await _eventStream.PublishAsync(new CustomerEvent(
            isNew ? "CustomerCreated" : "CustomerModified",
            customer));
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
