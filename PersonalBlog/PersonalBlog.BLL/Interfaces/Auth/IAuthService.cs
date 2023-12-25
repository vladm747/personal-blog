using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Interfaces.Auth;

public interface IAuthService
{
    Task<string> Register(RegisterDTO model);
    Task<User> Login(LoginDTO model);
    Task SignOut();
}