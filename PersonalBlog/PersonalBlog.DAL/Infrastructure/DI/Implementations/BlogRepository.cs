using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations.Base;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class BlogRepository: IBlogRepository
{
    private readonly PersonalBlogContext _context;
    public BlogRepository(PersonalBlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        return await _context.Blogs
            .Include(blog => blog.User)
            .Include(blog => blog.Posts).ToListAsync();
    }

    public async Task<Blog> CreateAsync(Blog entity)
    {
        var blogEntry = await _context.Blogs.AddAsync(entity);
        await _context.SaveChangesAsync();
        return blogEntry.Entity;
    }

    public async Task<Blog> GetByIdAsync(int blogId)
    {
        var blog = await _context.Blogs.FindAsync(blogId);
        
        return blog;
    }

    public async Task DeleteAsync(Blog entity)
    {
        _context.Blogs.Remove(entity);
        await _context.SaveChangesAsync();
    }
}