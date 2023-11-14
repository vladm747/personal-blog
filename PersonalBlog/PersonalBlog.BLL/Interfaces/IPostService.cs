using System.Security.Claims;
using PersonalBlog.BLL.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetAllAsync(int blogId);
    Task<PostDTO> GetByIdAsync(int postId);
    Task CreateAsync(ClaimsPrincipal userPrincipal, PostDTO entity);
    Task DeleteAsync(int postId);
    Task UpdateAsync(PostDTO entity);
}