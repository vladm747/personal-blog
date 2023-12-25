using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Interfaces.Auth;

public interface IUserService
{
    UserDTO GetCurrentUser(ClaimsPrincipal userPrincipal);
    User GetUserById(ClaimsPrincipal userPrincipal); 
    Task<IEnumerable<UserDTO>> GetAllAsync();
    string GetUserId(ClaimsPrincipal userPrincipal);
    IEnumerable<string> GetUsersEmails(IEnumerable<string> ids);
    string GetNickName(ClaimsPrincipal userPrincipal);
    Task<IdentityResult> DeleteAsync(ClaimsPrincipal user, string email);
}