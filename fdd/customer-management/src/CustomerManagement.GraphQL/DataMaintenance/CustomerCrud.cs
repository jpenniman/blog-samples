using HotChocolate;
using HotChocolate.Types;
using JetBrains.Annotations;
using Northwind.CustomerManagement.Api.DataMaintenance;

namespace Northwind.CustomerManagement.GraphQL.DataMaintenance;

/// <summary>
/// Crud commands
/// </summary>
[UsedImplicitly]
[ExtendObjectType(OperationTypeNames.Mutation)]
class CustomerCrud
{
    /// <summary>
    /// Save the provided customer into the system.
    /// </summary>
    /// <param name="customerService"></param>
    /// <param name="customer"></param>
    /// <returns></returns>
    public async Task<bool> SaveCustomer([Service] ICustomerService customerService, Customer customer)
    {
        await customerService.CreateNewCustomerAsync(customer);
        return true;
    }
}