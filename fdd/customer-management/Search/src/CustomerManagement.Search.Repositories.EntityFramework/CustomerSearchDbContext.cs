using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Search.Repositories.EntityFramework;

class CustomerSearchDbContext : DbContext
{
    public CustomerSearchDbContext(DbContextOptions<CustomerSearchDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; [UsedImplicitly] private set; } = null!;
    public DbSet<Address> Addresses { get; [UsedImplicitly] private set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSnakeCaseNamingConvention() // snake_case
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // this is a readonly model
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("customer_mgmt");

        var customer = modelBuilder.Entity<Customer>().ToTable("customers");
        customer.HasKey(c => c.Id);
        customer.Property(c => c.Id).HasColumnName("customer_id");
        customer.Property(c => c.CompanyName);
        customer.Property(c => c.PhoneNumber);
    }
}
