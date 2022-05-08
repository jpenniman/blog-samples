using System.ServiceModel;

namespace CustomerManagement.Search.Grpc.Sdk;

[ServiceContract]
public interface ICustomerSearchService
{
    [OperationContract]
    Task<CustomerSearchResponse> FindByCompanyNameAsync(CustomerSearchByCompanyNameRequest request);
}