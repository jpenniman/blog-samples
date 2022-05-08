namespace CustomerManagement.Search.Grpc.Client;

[PublicAPI]
public sealed class CustomerSearchClientSettings
{
    public static readonly string SECTION_NAME = "CustomerSearchService";
    public Uri? Server { get; set; }

    public TimeSpan CircuitBreakerDuration { get; set; } = TimeSpan.FromMinutes(10);
}