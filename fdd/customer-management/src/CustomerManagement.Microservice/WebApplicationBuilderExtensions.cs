using Microsoft.EntityFrameworkCore;
using Northwind.CustomerManagement.GraphQL;
using Northwind.CustomerManagement.Search;

namespace Northwind.CustomerManagement.Microservice;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterComponents(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddCustomerManagement(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("CustomerManagement")))
            .AddCustomerSearch(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("CustomerManagement")));
        builder.Services.AddGraphQLServer()
            .AddCustomerManagement();

        return builder;
    }
}