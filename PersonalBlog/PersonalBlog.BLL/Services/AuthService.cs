using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.DTO;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Services;

public class AuthService: IAuthService

{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _loginManager;

    public AuthService(UserManager<User> userMenager, RoleManager<IdentityRole> roleManager, SignInManager<User> loginManager)
    {
        _userManager = userMenager;
        _roleManager = roleManager;
        _loginManager = loginManager;
    }
    
    public async Task Register(RegisterDTO user)
    {
        var result = await _userManager.CreateAsync(new User
        {
            NickName = user.NickName,
            UserName = user.Email,
            Email = user.Email,
        }, user.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(';', result.Errors.Select(x => x.Description)));
        }
        else
        {
            var userToCreate = await _userManager.FindByEmailAsync(user.Email);
            var roleResult = await _userManager.AddToRoleAsync(userToCreate, user.Role);
        }
    }
    
    public async Task<User> Login(LoginDTO login)
    {
        var user = _userManager.Users.SingleOrDefault(login => login.UserName == login.Email);

        if (user == null)
        {
            throw new System.Exception($"User with email: '{login.Email}' is not found.");
        }
                
        return await _userManager.CheckPasswordAsync(user, login.Password) ? user : throw new ArgumentException("Wrong Password");
    }
   
    public async Task SignOut()
    {
        await _loginManager.SignOutAsync();
    }
}