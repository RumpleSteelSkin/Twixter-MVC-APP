using System.Linq.Expressions;
using CorePackage.Entities;

namespace CorePackage.Repositories;

// ReSharper disable once TypeParameterCanBeVariant
public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool include = true, bool enableTracking = true, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool include = true, bool enableTracking = true, CancellationToken cancellationToken = default);
    
    Task<TEntity?> FindByIdAsync(TId id, bool enableTracking = true, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}