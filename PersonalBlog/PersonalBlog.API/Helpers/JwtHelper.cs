using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.DAL.Entities.Auth;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PersonalBlog.API.Helpers;

public class JwtHelper
{
    public static string GenerateJwt(User user, IEnumerable<string> roles, JwtSettings jwtSettings)
    {
        if (user == null) throw new Exception($"Jwt generation not proceeded - {nameof(user)} is null");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.NickName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "ADMIN"),
            new Claim(ClaimTypes.Role, "AUTHOR"),
            new Claim(ClaimTypes.Role, "USER")
        };

        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.ExpirationInDays));

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Issuer,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}