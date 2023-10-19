using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonalBlog.API.Helpers;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.Interfaces;

namespace PersonalBlog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;
    
    public AuthController(IAuthService authService,
        IOptionsSnapshot<JwtSettings> jwtSettings,
        IRoleService roleService)
    {
        _roleService = roleService;
        _authService = authService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        await _authService.Register(model);

        return Created(string.Empty, string.Empty);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        var user = await _authService.Login(model);

        var roles = await _roleService.GetRoles(user);
        var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);
      
        HttpContext.Response.Cookies.Append("JWT", token,
          new CookieOptions
          {
              MaxAge = TimeSpan.FromDays(30),
              SameSite = SameSiteMode.None,
              Secure = true
          });

        return Ok(token);
    }

    [HttpPost("signout")]
    [Authorize]
    public async Task<IActionResult> SignOut()
    {
        await _authService.SignOut();
        HttpContext.Response.Cookies.Append("JWT", "",
             new CookieOptions
             {
                 MaxAge = TimeSpan.FromMilliseconds(1),
                 SameSite = SameSiteMode.None,
                 Secure = true
             });

        return Ok();
    }
}