namespace Northwind.CustomerManagement.Api.DataMaintenance;

public interface ICustomerService
{
    Task CreateNewCustomerAsync(Customer customer);
    
    Task UpdateCustomerAsync(Customer customer);
    
    Task DeleteCustomerAsync(Customer customer);

    Task<Customer?> GetAsync(long id);
}
