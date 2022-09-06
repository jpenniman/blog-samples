using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Northwind.CustomerManagement.Api.DataMaintenance;

namespace Northwind.CustomerManagement.DataMaintenance.Services.Impl;

class InProcessEventStream : ICustomerEventStream, ICustomerEventStreamPublisher
{
    readonly Subject<CustomerEvent> _stream = new();

    public IObservable<CustomerEvent> Stream => _stream;

    public Task PublishAsync(CustomerEvent customerEvent)
    {
        _stream.OnNext(customerEvent);
        return Task.CompletedTask;
    }
}