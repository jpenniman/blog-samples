using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Search.GraphQL;

/// <summary>
/// Adds customer management GraphQL components to the builder.
/// </summary>
[PublicAPI]
public static class RequestExecutorBuilderExtensions
{
    /// <summary>
    /// Adds customer management GraphQL components to the builder.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static IRequestExecutorBuilder AddCustomerManagementSearchGraphQLTypes(this IRequestExecutorBuilder builder)
    {
        return builder.AddTypeExtension<SearchQuery>();
    }
}