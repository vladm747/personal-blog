using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO.Auth;

namespace PersonalBlog.BLL.Interfaces.Auth;

public interface IUserService
{
    public Task<IdentityResult> DeleteAsync(ClaimsPrincipal user, string email);
}