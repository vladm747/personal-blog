using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.BLL.Helpers;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Interfaces.Auth;

public interface ITokenService
{
    Task<IdentityResult> UpdateUserRefreshToken(User user, RefreshToken token);
    string GenerateJwtToken(User user, IEnumerable<string> roles, JwtSettings jwtSettings);
    RefreshToken GenerateRefreshToken(JwtSettings jwtSettings);
}