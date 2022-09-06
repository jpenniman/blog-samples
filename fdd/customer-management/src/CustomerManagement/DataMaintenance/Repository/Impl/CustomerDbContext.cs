using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Northwind.CustomerManagement.DataMaintenance.Domain;

namespace Northwind.CustomerManagement.DataMaintenance.Repository.Impl;

#nullable disable
class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options) { }

    internal DbSet<Customer> Customers { get; [UsedImplicitly] private set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSnakeCaseNamingConvention(); // snake_case
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("customer_mgmt");

        modelBuilder.Entity<Customer>()
            .Property(c => c.CompanyName).HasMaxLength(50);
        modelBuilder.Entity<Customer>()
            .Property(c => c.PhoneNumber).HasMaxLength(24);
    }
}
