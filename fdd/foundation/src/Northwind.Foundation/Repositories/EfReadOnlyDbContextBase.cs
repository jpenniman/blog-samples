using Microsoft.EntityFrameworkCore;

namespace Northwind.Foundation.Repositories;

public abstract class EfReadOnlyDbContextBase : DbContext
{
    static readonly string READ_ONLY_MESSAGE = "Saving is not allows in a readonly context.";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException(READ_ONLY_MESSAGE);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException(READ_ONLY_MESSAGE);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new InvalidOperationException(READ_ONLY_MESSAGE);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new InvalidOperationException(READ_ONLY_MESSAGE);
    }
    
}