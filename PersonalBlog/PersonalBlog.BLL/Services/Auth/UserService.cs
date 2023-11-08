using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Services.Auth;

public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> manager)
    {
        _userManager = manager;
    }

    public async Task<IdentityResult> DeleteAsync(ClaimsPrincipal userPrincipal, string email)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
            throw new Exception("cant get user");

        if (user.Email != email)
            throw new UnauthorizedAccessException("You can't delete another user.");

        var result = await _userManager.DeleteAsync(user);
        return result;
    }
}