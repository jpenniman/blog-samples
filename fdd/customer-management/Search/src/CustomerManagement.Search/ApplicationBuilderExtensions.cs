using CustomerManagement.Search.Api;
using CustomerManagement.Search.Services;
using Microsoft.AspNetCore.Builder;
using ProtoBuf.Grpc.Reflection;
using ProtoBuf.Meta;

namespace CustomerManagement.Search.Grpc;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomerManagementSearchGrpcServices(this IApplicationBuilder app)
    {
        app.UseEndpoints(ep => ep.MapGrpcService<CustomerSearchService>());

        return app;
    }
}