using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.BLL.Exceptions;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.DAL.Entities.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.BLL.Helpers;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PersonalBlog.BLL.Services.Auth;

public class TokenService: ITokenService
{
    private readonly UserManager<User> _userManager;

    public TokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> UpdateUserRefreshToken(User user, RefreshToken token)
    {
        if (user == null)
            throw new UserNotFoundException("User Not Found");

        user.RefreshToken = token.Token;
        user.TokenCreated = token.Created;
        user.TokenExpires = token.Expires;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new UserTokenUpdateException("Can't update user. TokenService.UpdateUserRefreshToken");
        
        return result;
    }
    public string GenerateJwtToken(User user, IEnumerable<string> roles, JwtSettings jwtSettings)
    {
        if (user == null) throw new Exception($"Jwt generation not proceeded - {nameof(user)} is null");

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (ClaimTypes.Name, user.NickName!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Role, "author"),
            new (ClaimTypes.Role, "user")
        };

        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(Convert.ToDouble(jwtSettings.ExpirationInHours));

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Issuer,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(JwtSettings jwtSettings)
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };
        
        return refreshToken;
    }
}