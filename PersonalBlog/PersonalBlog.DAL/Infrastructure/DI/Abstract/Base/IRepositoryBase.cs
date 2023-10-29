namespace PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

public interface IRepositoryBase<TKey, TEntity>
{
    IEnumerable<TEntity> GetAll();
    Task<TEntity?> FindByKeyAsync(TKey key);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}