using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class PostRepository: RepositoryBase<int, Post>, IPostRepository
{
    public PostRepository(PersonalBlogContext context) : base(context) { }
    
    public async Task<IEnumerable<Post>> GetAllAsync(int blogId)
    {
        return await Table.Select(item => item).Where(item => item.BlogId == blogId).ToListAsync();
    } 
}