using Microsoft.AspNetCore.Identity;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.BLL.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<IdentityRole>> GetRoles();
    Task<IEnumerable<string>> GetRoles(User user);
    Task CreateRole(string roleName);
}