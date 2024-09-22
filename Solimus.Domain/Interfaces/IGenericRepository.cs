namespace Solimus.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class 
{
    Task<TEntity?> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    void Delete(TEntity entity);
}
