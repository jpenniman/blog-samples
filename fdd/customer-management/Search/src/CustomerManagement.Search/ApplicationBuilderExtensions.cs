using CustomerManagement.Search.Services;
using Microsoft.AspNetCore.Builder;

namespace CustomerManagement.Search;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomerManagementSearchGrpcServices(this IApplicationBuilder app)
    {
        app.UseEndpoints(ep => ep.MapGrpcService<CustomerSearchService>());

        return app;
    }
}