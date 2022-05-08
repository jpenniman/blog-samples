using CustomerManagement.Search.Grpc.Sdk;
using CustomerManagement.Search.Grpc.Server.Services;
using Microsoft.AspNetCore.Builder;
using ProtoBuf.Grpc.Reflection;
using ProtoBuf.Meta;

namespace CustomerManagement.Search.Grpc.Server;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomerManagementSearchGrpcServices(this IApplicationBuilder app)
    {
        app.UseEndpoints(ep => ep.MapGrpcService<CustomerSearchService>());
        
        var generator = new SchemaGenerator
        {
            ProtoSyntax = ProtoSyntax.Proto3
        };

        var schema = generator.GetSchema<ICustomerSearchService>();

        // using var writer = new System.IO.StreamWriter("services.proto");
        // writer.Write(schema);
        return app;
    }
}