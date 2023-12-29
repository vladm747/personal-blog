using System.Security.Claims;
using PersonalBlog.Common.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetAllAsync(int blogId);
    Task<PostDTO> GetByIdAsync(int postId);
    Task<PostDTO?> CreateAsync(PostDTO entity);
    Task<int> DeleteAsync(int postId);
    Task<int> UpdateAsync(PostDTO entity);
}