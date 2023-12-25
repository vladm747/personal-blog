using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PersonalBlog.Common.DTO.Auth;
using PersonalBlog.BLL.Exceptions;
using PersonalBlog.BLL.Helpers;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Services.Auth;

public class AuthService: IAuthService

{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _loginManager;
    private readonly IBlogService _blogService;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<User> userMenager, RoleManager<IdentityRole> roleManager, 
        SignInManager<User> loginManager, IBlogService blogService, ITokenService tokenService,
        IOptionsSnapshot<JwtSettings> jwtSettings)
    {
        _userManager = userMenager;
        _roleManager = roleManager;
        _loginManager = loginManager;
        _blogService = blogService;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<string> Register(RegisterDTO user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken(_jwtSettings);
        
        var result = await _userManager.CreateAsync(new User
        {
            NickName = user.NickName,
            UserName = user.Email,
            Email = user.Email,
            RefreshToken = refreshToken.Token,
            TokenExpires = refreshToken.Expires,
            TokenCreated = refreshToken.Created
        }, user.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(';', result.Errors.Select(x => x.Description)));
        }
        else
        {
            var userToCreate = await _userManager.FindByEmailAsync(user.Email);
            
            var toCreate = userToCreate ?? throw new UserNotFoundException("Can't find user with such email");
            
            var roleResult = await _userManager.AddToRoleAsync(toCreate, user.Role);
            var roles = await _userManager.GetRolesAsync(toCreate);
            
            if (roles.Contains("author"))
            {
                await _blogService.CreateAsync(toCreate.Id);
            }
            return userToCreate.Id;
        }
    }
    
    public async Task<User> Login(LoginDTO login)
    {
        var user = _userManager.Users.SingleOrDefault(item => item.UserName == login.Email);

        if (user == null)
        {
            throw new UserNotFoundException($"User with email: '{login.Email}' is not found.");
        }
                
        return await _userManager.CheckPasswordAsync(user, login.Password) ? user : throw new ArgumentException("Wrong Password");
    }
   
    public async Task SignOut()
    {
        await _loginManager.SignOutAsync();
    }
    
    
}