using System.Globalization;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Northwind.CustomerManagement.Api.DataMaintenance;
using Northwind.CustomerManagement.GraphQL.DataMaintenance;
using Northwind.CustomerManagement.GraphQL.Search;
using Northwind.CustomerManagement.Search.GraphQL;

namespace Northwind.CustomerManagement.GraphQL;

/// <summary>
/// Adds customer management GraphQL components to the builder.
/// </summary>
public static class RequestExecutorBuilderExtensions
{
    /// <summary>
    /// Adds customer management GraphQL components to the builder.
    /// </summary>
    public static IRequestExecutorBuilder AddCustomerManagement(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddQueryType(q => q.Name(OperationTypeNames.Query))
            .AddTypeExtension<SearchQuery>()
            .AddTypeExtension<CrudQuery>()
            .AddMutationType(m => m.Name(OperationTypeNames.Mutation))
            .AddTypeExtension<CustomerCrud>();
    }
}