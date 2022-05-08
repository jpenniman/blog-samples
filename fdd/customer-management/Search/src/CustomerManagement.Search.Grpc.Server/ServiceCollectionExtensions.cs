using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace CustomerManagement.Search.Grpc.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerManagementSearchGrpcServer(this IServiceCollection services)
    {
        services.AddCodeFirstGrpc();
        return services;
    }
}