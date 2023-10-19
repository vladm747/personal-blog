using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.DAL.Entities.Auth;

public class User: IdentityUser
{
    [MaxLength(40)] 
    public string? NickName { get; set; }
}