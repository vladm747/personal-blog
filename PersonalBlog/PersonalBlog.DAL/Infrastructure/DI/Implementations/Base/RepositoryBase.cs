using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

public abstract class RepositoryBase<TKey, TEntity> : IRepositoryBase<TKey, TEntity> 
    where TEntity: class
    where TKey : IEquatable<TKey>
{
    private readonly bool _disposeContext;
    private bool _isDisposed;
    private DbContext Context { get; set; }
    protected DbSet<TEntity> Table { get; }

    protected RepositoryBase(DbContext dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Table = Context.Set<TEntity>();
        _disposeContext = false;
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
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity item)
    {
        Context.Entry(item).State = EntityState.Modified;
       await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity item)
    {
        Table.Remove(item);
        await Context.SaveChangesAsync();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing && _disposeContext)
            Context.Dispose();

        _isDisposed = true;
    }

    ~RepositoryBase()
    {
        Dispose(true);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}