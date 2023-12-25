using PersonalBlog.Common.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDTO>> GetAllAsync(int postId);
    Task<CommentDTO> GetByIdAsync(int commentId);
    Task CreateAsync(CommentDTO entity);
    Task DeleteAsync(int commentId);
    Task UpdateAsync(CommentDTO entity);
}