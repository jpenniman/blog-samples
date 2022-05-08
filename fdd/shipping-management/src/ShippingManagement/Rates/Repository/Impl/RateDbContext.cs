using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Northwind.ShippingManagement.Rates.Domain;

namespace Northwind.ShippingManagement.Rates.Repository.Impl;

#nullable disable

class RateDbContext : DbContext
{
    public RateDbContext(DbContextOptions<RateDbContext> options) : base(options)
    {
    }
    
    public DbSet<Rate> ShippingRates { get; [UsedImplicitly] private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("shipping_mgmt");
    }
}