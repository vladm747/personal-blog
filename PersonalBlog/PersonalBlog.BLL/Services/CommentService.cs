using AutoMapper;
using PersonalBlog.Common.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.BLL.Services;

public class CommentService: ICommentService
{
    private readonly ICommentRepository _repo;
    private readonly IMapper _mapper;
    
    public CommentService(ICommentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }


    public async Task<IEnumerable<CommentDTO>> GetAllAsync(int postId)
    {
        var comments = await _repo.GetAllAsync(postId);

        return _mapper.Map<IEnumerable<CommentDTO>>(comments);
     }

    public async Task<CommentDTO> GetByIdAsync(int commentId)
    {
        var comment = await _repo.FindByKeyAsync(commentId);
       
        if(comment == null)
            throw new KeyNotFoundException($"The comment with key {commentId} does not exist in database!");

        return _mapper.Map<CommentDTO>(comment);
    }

    public async Task CreateAsync(CommentDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException("the entity you are trying create is null");
        
        var comment = _mapper.Map<Comment>(entity);
        await _repo.CreateAsync(comment);
    }

    public async Task DeleteAsync(int commentId)
    {
        var entity = await _repo.FindByKeyAsync(commentId);

        if (entity == null)
            throw new InvalidOperationException("The entity you are trying to delete does not exist in database!");
        
        var comment = _mapper.Map<Comment>(entity);
        await _repo.DeleteAsync(comment);
    }

    public async Task UpdateAsync(CommentDTO entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity is null.");
        
        var comment = _mapper.Map<Comment>(entity);
        await _repo.UpdateAsync(comment);
    }
}