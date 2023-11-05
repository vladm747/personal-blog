using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.BLL.Services;

public class PostService: IPostService
{
    private readonly IPostRepository _repo;
    private readonly IMapper _mapper;
    
    public PostService(IPostRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PostDTO>> GetAllAsync(int blogId)
    {
        var posts = await _repo.GetAllAsync(blogId);

        return _mapper.Map<IEnumerable<PostDTO>>(posts);
    }

    public async Task<PostDTO> GetByIdAsync(int postId)
    {
        var post = await _repo.FindByKeyAsync(postId);
       
        if (post == null)
            throw new KeyNotFoundException($"The post with key {postId} does not exist in database!");

        return _mapper.Map<PostDTO>(post);
    }

    public async Task CreateAsync(PostDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException("the entity you are trying create is null");
        
        var post = _mapper.Map<Post>(entity);
        await _repo.CreateAsync(post);
    }

    public async Task DeleteAsync(int postId)
    {
        var entity = await GetByIdAsync(postId);

        if (entity == null)
            throw new InvalidOperationException("The entity you are trying to delete does not exist in database!");
        
        var post = _mapper.Map<Post>(entity);
        await _repo.DeleteAsync(post);
    }

    public async Task UpdateAsync(PostDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity is null.");
        
        var post = _mapper.Map<Post>(entity);
        await _repo.UpdateAsync(post);
    }
}