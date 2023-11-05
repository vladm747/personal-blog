using AutoMapper;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.Interfaces;
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
}