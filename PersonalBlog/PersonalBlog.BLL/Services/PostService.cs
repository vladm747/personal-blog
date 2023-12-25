using AutoMapper;
using PersonalBlog.Common.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.BLL.Subscription.Interfaces;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.BLL.Services;

public class PostService: IPostService
{
    private readonly IPostRepository _repo;
    private readonly IMapper _mapper;
    private readonly ISubscriptionService _subscription;
    
    public PostService(IPostRepository repo, IMapper mapper, ISubscriptionService subscription,
        IUserService userService)
    {
        _repo = repo;
        _mapper = mapper;
        _subscription = subscription;
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
        try
        {
            await _repo.CreateAsync(post);
        }
        finally
        {
            _subscription.Notify(entity.UserNickName, entity.BlogId);
        }
    }

    public async Task DeleteAsync(int postId)
    {
        var entity = await _repo.FindByKeyAsync(postId);

        if (entity == null)
            throw new InvalidOperationException("The entity you are trying to delete does not exist in database!");
        
        await _repo.DeleteAsync(entity);
    }

    public async Task UpdateAsync(PostDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity is null.");
        
        var post = _mapper.Map<Post>(entity);
        await _repo.UpdateAsync(post);
    }
}