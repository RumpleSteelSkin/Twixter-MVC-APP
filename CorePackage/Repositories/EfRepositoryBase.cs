using System.Linq.Expressions;
using CorePackage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CorePackage.Repositories;

public abstract class EfRepositoryBase<TEntity, TId, TContext> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected readonly DbSet<TEntity> DbSet;
    private readonly ILogger<EfRepositoryBase<TEntity, TId, TContext>> _logger;
    private readonly IMemoryCache? _cache;
    private readonly bool _useCaching;
    private readonly bool _useSoftDelete;

    protected EfRepositoryBase(TContext context, ILogger<EfRepositoryBase<TEntity, TId, TContext>> logger, IMemoryCache? cache = null, bool useCaching = false, bool useSoftDelete = false)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = Context.Set<TEntity>();
        _logger = logger;
        _cache = cache;
        _useCaching = useCaching;
        _useSoftDelete = useSoftDelete;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding entity: {Entity}", entity);
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        _cache?.Remove(typeof(TEntity).Name);  
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating entity: {Entity}", entity);
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        _cache?.Remove(typeof(TEntity).Name);
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (_useSoftDelete)
        {
            _logger.LogInformation("Soft deleting entity: {Entity}", entity);
            entity.IsDeleted = true;
            DbSet.Update(entity);
        }
        else
        {
            _logger.LogInformation("Hard deleting entity: {Entity}", entity);
            DbSet.Remove(entity);
        }

        await Context.SaveChangesAsync(cancellationToken);
        _cache?.Remove(typeof(TEntity).Name);
        return entity;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;
        if (filter != null)
            query = query.Where(filter);
        if (!enableTracking)
            query = query.AsNoTracking();
        if (_useSoftDelete)
            query = query.Where(e => !e.IsDeleted);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool include = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;

        if (!enableTracking)
            query = query.AsNoTracking();

        if (include)
            query = IncludeNavigationProperties(query);

        if (_useSoftDelete)
            query = query.Where(e => !e.IsDeleted);

        return await query.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool include = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        string cacheKey = $"{typeof(TEntity).Name}_List";
        if (_useCaching && _cache != null && _cache.TryGetValue(cacheKey, out List<TEntity>? cachedList))
        {
            return cachedList!;
        }

        IQueryable<TEntity> query = DbSet;

        if (!enableTracking)
            query = query.AsNoTracking();

        if (include)
            query = IncludeNavigationProperties(query);

        if (filter != null)
            query = query.Where(filter);

        if (_useSoftDelete)
            query = query.Where(e => !e.IsDeleted);

        var result = await query.ToListAsync(cancellationToken);

        if (_useCaching && _cache != null)
        {
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        }

        return result;
    }

    public async Task<TEntity?> FindByIdAsync(TId id, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;
        if (!enableTracking)
            query = query.AsNoTracking();

        if (_useSoftDelete)
            query = query.Where(e => !e.IsDeleted);

        return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;
        if (filter != null)
            query = query.Where(filter);

        if (_useSoftDelete)
            query = query.Where(e => !e.IsDeleted);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> IncludeNavigationProperties(IQueryable<TEntity> query)
    {
        var entityType = Context.Model.FindEntityType(typeof(TEntity));
        if (entityType == null) return query;

        foreach (var navigation in entityType.GetNavigations())
        {
            query = query.Include(navigation.Name);
        }

        return query;
    }
}
