using PersonalBlog.BLL.DTO;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Interfaces;

public interface IAuthService
{
    Task Register(RegisterDTO model);
    Task<User> Login(LoginDTO model);
    Task SignOut();
}