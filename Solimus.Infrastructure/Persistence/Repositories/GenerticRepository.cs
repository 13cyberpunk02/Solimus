using Microsoft.EntityFrameworkCore;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;

namespace Solimus.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(SolimusContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetById(Guid id) => await _dbSet.FindAsync(id);

    public TEntity Update(TEntity entity)
    {
        var entry = _dbSet.Update(entity);
        return entry.Entity;
    }
}
