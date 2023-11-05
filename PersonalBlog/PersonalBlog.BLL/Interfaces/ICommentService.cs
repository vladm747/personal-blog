using PersonalBlog.BLL.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDTO>> GetAllAsync(int postId);
    Task<CommentDTO> GetByIdAsync(int commentId);
}