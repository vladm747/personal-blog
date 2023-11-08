using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Abstract;

public interface IBlogRepository
{
    Task<IEnumerable<Blog>> GetAllAsync();
    Task<Blog> CreateAsync(Blog entity);
    Task<Blog> GetByIdAsync(int blogId);
    Task DeleteAsync(Blog entity);
}