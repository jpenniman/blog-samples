using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Northwind.ShippingManagement.Grpc.Sdk;
using ProtoBuf.Grpc.Reflection;
using ProtoBuf.Grpc.Server;
using ProtoBuf.Meta;

namespace Northwind.ShippingManagement.Grpc.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShippingServiceGrpcServer(this IServiceCollection services)
    {
        services.AddCodeFirstGrpc();
        
        return services;
    }

    public static IApplicationBuilder MapShippingManagementGrpcServices(this IApplicationBuilder app)
    {
        app.UseEndpoints(ep => ep.MapGrpcService<FreightCalculatorGrpcService>());
        
        var generator = new SchemaGenerator
        {
            ProtoSyntax = ProtoSyntax.Proto3
        };

        var schema = generator.GetSchema<IFreightCalculatorService>();

        using var writer = new System.IO.StreamWriter("services.proto");
        writer.Write(schema);
        return app;
    }
}