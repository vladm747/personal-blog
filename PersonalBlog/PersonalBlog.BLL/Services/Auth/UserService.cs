using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.BLL.DTO.Auth;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Services.Auth;

public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;
    private IUserService _userServiceImplementation;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> manager, IMapper mapper)
    {
        _userManager = manager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }
    //TODO handle null possibility
    public string GetUserId(ClaimsPrincipal userPrincipal) =>
        _userManager.GetUserId(userPrincipal);

    public IEnumerable<string> GetUsersEmails(IEnumerable<string> ids) => 
        _userManager.Users.Where(user => ids.Contains(user.Id))
            .Select(item => item.Email).AsNoTracking().ToList();
    
    public string GetNickName(ClaimsPrincipal userPrincipal)
    {
        var userId = _userManager.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException("Cant get user");
        
        var nickName = _userManager.Users.Where(user => user.Id == userId).
            Select(item => item.NickName).AsNoTracking().FirstOrDefault();
        
        return nickName;
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