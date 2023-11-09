using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

public abstract class RepositoryBase<TKey, TEntity> : IRepositoryBase<TKey, TEntity> where TEntity: class
{
    private readonly DbContext _dbContext;
    public DbSet<TEntity> Table { get; }

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Table = _dbContext.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Table;
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public virtual async Task<TEntity> FindByKeyAsync(TKey key)
    {
        return await Table.FindAsync(key);
    }

    public async Task CreateAsync(TEntity item)
    {
        var entityEntry = await Table.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity item)
    {
       _dbContext.Entry(item).State = EntityState.Modified;
       await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity item)
    {
        Table.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
}