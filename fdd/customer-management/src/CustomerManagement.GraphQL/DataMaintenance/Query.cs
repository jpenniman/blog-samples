using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using JetBrains.Annotations;
using Northwind.CustomerManagement.Api;
using Northwind.CustomerManagement.Api.DataMaintenance;
using Northwind.CustomerManagement.Api.Search;

namespace Northwind.CustomerManagement.Search.GraphQL;

/// <summary>
/// GraphQL Query object
/// </summary>
[UsedImplicitly]
[ExtendObjectType(OperationTypeNames.Query)]
class CrudQuery
{
    /// <summary>
    /// Fetch a customer by ID.
    /// </summary>
    /// <param name="customerSearch"></param>
    /// <param name="companyName"></param>
    /// <returns></returns>
    public Task<Customer?> GetCustomer([Service] ICustomerService customerService, long id)
    {
        return customerService.GetAsync(id);
    }
}