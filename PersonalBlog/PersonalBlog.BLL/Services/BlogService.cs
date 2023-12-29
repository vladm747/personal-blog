using AutoMapper;
using PersonalBlog.Common.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.BLL.Services;

public class BlogService: IBlogService
{
    private readonly IBlogRepository _repo;

    private readonly IMapper _mapper;
    
    public BlogService(IBlogRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BlogDTO>> GetAllAsync()
    {
        var blogs = await _repo.GetAllAsync();

        return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
    }
    
    public async Task<BlogDTO> GetByIdAsync(int blogId)
    {
        var blog = await _repo.GetByIdAsync(blogId);

        if (blog == null)
            throw new KeyNotFoundException($"The blog with id {blogId} does not exist in database.");
        
        return _mapper.Map<BlogDTO>(blog);
    }

    public async Task<BlogDTO> CreateAsync(string userId)
    {
        var entity = new Blog()
        {
            UserId = userId
        };

        var blog = await _repo.CreateAsync(entity);
        return _mapper.Map<BlogDTO>(blog);
    }

    public async Task<int> DeleteAsync(int blogId)
    {
        var blog = await _repo.GetByIdAsync(blogId);

        if (blog == null)
            throw new KeyNotFoundException($"There is no blog with ID {blogId} in database.");

        return await _repo.DeleteAsync(blog);
    }
}