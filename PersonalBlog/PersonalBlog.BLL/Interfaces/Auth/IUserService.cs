using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO.Auth;

namespace PersonalBlog.BLL.Interfaces.Auth;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    string GetUserId(ClaimsPrincipal userPrincipal);
    IEnumerable<string> GetUsersEmails(IEnumerable<string> ids);
    string GetNickName(ClaimsPrincipal userPrincipal);
    Task<IdentityResult> DeleteAsync(ClaimsPrincipal user, string email);
}