using CustomerManagement.Search.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Foundation.Repositories;

namespace CustomerManagement.Search.Repositories.EntityFramework;

class CustomerSearchEfRepository : ICustomerSearchRepository
{
    readonly CustomerSearchDbContext _searchDbContext;
    readonly ILogger<CustomerSearchEfRepository> _logger;

    public CustomerSearchEfRepository(CustomerSearchDbContext dbContext, ILogger<CustomerSearchEfRepository> logger)
    {
        _searchDbContext = dbContext;
        _logger = logger;
    }

    public async IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName)
    {

        var results = _searchDbContext.Customers
            .TagWithCallSite()
            .TagWith($"{nameof(ICustomerSearchRepository)}.{nameof(FindByCompanyName)}")
            .Where(c => c.CompanyName.StartsWith(companyName))
            .Select(r => new CustomerSearchResult(r.Id, r.CompanyName, r.PhoneNumber, null))
            .AsAsyncEnumerable()
            .WithCancellation(CancellationToken.None);

        // yield return cannot be inside a try/catch, so we have to get the enumerator and iterate over the
        // async enumerable. This allows the materialization of the enumerable to be in side a try
        // and catch any database errors.
        var enumerator = results.GetAsyncEnumerator();
        while (true)
        {
            try
            {
                if (! await enumerator.MoveNextAsync())
                    yield break;
            }
            catch (Exception ex)
            {
                const string msg = "An error occurred trying to find the customer by name.";
                _logger.LogDebug(ex, msg);
                throw new RepositoryException(msg, ex);
            }

            yield return enumerator.Current;
        }
    }
}