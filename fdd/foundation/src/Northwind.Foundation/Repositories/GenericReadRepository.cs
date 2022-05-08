using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Foundation.Repositories;

public abstract class GenericReadRepository<TEntity, TKey> : IReadRepository<TEntity, TKey> where TEntity : class
{
    readonly DbContext _dbContext;

    protected GenericReadRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public ValueTask<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (id is object?[] compositeKey)
            return _dbContext.Set<TEntity>().FindAsync(compositeKey, cancellationToken: cancellationToken);
        
        return _dbContext.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int skip = 0, int take = 100, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _dbContext.Set<TEntity>().Where(filter).Skip(skip).Take(take).AsAsyncEnumerable();
    }
}