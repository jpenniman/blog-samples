using System.Threading.Tasks;
using Northwind.CustomerManagement.Api.DataMaintenance;

namespace Northwind.CustomerManagement.DataMaintenance.Services;

interface ICustomerEventStreamPublisher
{
    Task PublishAsync(CustomerEvent customerEvent); 
}
