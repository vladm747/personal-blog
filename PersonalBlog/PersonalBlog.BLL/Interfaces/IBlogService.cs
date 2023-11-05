using PersonalBlog.BLL.DTO;

namespace PersonalBlog.BLL.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<BlogDTO>> GetAllAsync();
    Task<BlogDTO> GetByIdAsync(int blogId);
    Task CreateAsync(string userId);
    Task DeleteAsync(int blogId);
}