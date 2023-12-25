using System.Security.Claims;
using PersonalBlog.Common.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetAllAsync(int blogId);
    Task<PostDTO> GetByIdAsync(int postId);
    Task CreateAsync(PostDTO entity);
    Task DeleteAsync(int postId);
    Task UpdateAsync(PostDTO entity);
}