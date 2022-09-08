using CustomerManagement.Search.Api;
using Microsoft.EntityFrameworkCore;
using Northwind.Foundation.Repositories;

namespace CustomerManagement.Search.Repositories.Impl;

class CustomerSearchEfRepository 
    : EfReadOnlyDbContextBase<CustomerSearchEfRepository>, 
        ICustomerSearchRepository
{
    public CustomerSearchEfRepository(DbContextOptions<CustomerSearchEfRepository> options) 
        : base(options)
    { }

    DbSet<CustomerSearchResult> ReadModel { get; [UsedImplicitly] set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var readModel = modelBuilder.Entity<CustomerSearchResult>()
            .ToTable("customers", "sales");

        readModel.HasKey(e => e.Id);
        readModel.Property(e => e.Id).HasColumnName("customer_id");
        readModel.Property(e => e.CompanyName).HasColumnName("company_name");
        readModel.Property(e => e.Phone).HasColumnName("phone");
        readModel.Property(e => e.PostalCode).HasColumnName("postal_code");
    }
    
    public IAsyncEnumerable<CustomerSearchResult> FindByCompanyName(string companyName)
    {
        if (!companyName.EndsWith('%'))
            companyName += '%';
        
        return ReadModel
            .Where(c => EF.Functions.Like(c.CompanyName, companyName))
            .AsAsyncEnumerable();
    }
}