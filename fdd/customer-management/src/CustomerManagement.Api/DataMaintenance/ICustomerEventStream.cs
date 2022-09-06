namespace Northwind.CustomerManagement.Api.DataMaintenance;

/// <summary>
/// Provides access to the Customer Event Stream.
/// </summary>
public interface ICustomerEventStream
{
    /// <summary>
    /// An observable of the underlying event stream.
    /// </summary>
    IObservable<CustomerEvent> Stream { get; }
}
