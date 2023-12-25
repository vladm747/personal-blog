using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonalBlog.API.Models;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.BLL.Helpers;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    
    public AuthController(IAuthService authService, IOptionsSnapshot<JwtSettings> jwtSettings,
        IUserService userService, IRoleService roleService, ITokenService tokenService)
    {
        _roleService = roleService;
        _authService = authService;
        _userService = userService;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        var userId = await _authService.Register(model);

        return Ok(userId);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        var user = await _authService.Login(model);
        var roles = await _roleService.GetRoles(user);
        var accessToken = _tokenService.GenerateJwtToken(user, roles, _jwtSettings);
        var refreshToken = _tokenService.GenerateRefreshToken(_jwtSettings);
        
        SetRefreshToken(refreshToken.Token);
        SetAccessToken(accessToken);
        
        var result = await _tokenService.UpdateUserRefreshToken(user, refreshToken);
        
        return Ok(new Tokens(){AccessToken = accessToken, RefreshToken = refreshToken.Token});
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var user = _userService.GetUserById(HttpContext.User);
        
        if (!user.RefreshToken.Equals(refreshToken))
            return Unauthorized("Invalid Refresh Token");
        
        if(user.TokenExpires < DateTime.Now)
            return Unauthorized("Token expired");
        
        var roles = await _roleService.GetRoles(user);

        string jwtToken = _tokenService.GenerateJwtToken(user, roles, _jwtSettings);
        var newRefreshToken = _tokenService.GenerateRefreshToken(_jwtSettings);
        
        SetRefreshToken(jwtToken);
        SetAccessToken(newRefreshToken.Token);
        
        var result = await _tokenService.UpdateUserRefreshToken(user, newRefreshToken);
        
        return Ok(jwtToken);
    }
    
    [HttpPost("sign-out")]
    [Authorize]
    public new async Task<IActionResult> SignOut()
    {
        await _authService.SignOut();
        
        SetAccessTokenNull();
        SetRefreshTokenNull();

        return Ok();
    }
    
    private void SetRefreshToken(string token)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7)
        };
        
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
    
    private void SetAccessToken(string accessToken)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddHours(_jwtSettings.ExpirationInHours)
        };
       
        Response.Cookies.Append("accessToken", accessToken, cookieOptions);
    }
    
    private void SetAccessTokenNull()
    {
        var cookieOptions = new CookieOptions
        {
            MaxAge = TimeSpan.FromMilliseconds(1),
            SameSite = SameSiteMode.None,
            Secure = true
        };
       
        Response.Cookies.Append("JWT", "", cookieOptions);
    }
    
    private void SetRefreshTokenNull()
    {
        var cookieOptions = new CookieOptions
        {
            MaxAge = TimeSpan.FromMilliseconds(1),
            SameSite = SameSiteMode.None,
            Secure = true
        };
        
        Response.Cookies.Append("refreshToken", "", cookieOptions);
    }
}