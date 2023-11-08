using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PersonalBlog.DAL.Entities.Auth;

public class User: IdentityUser
{
    public override string Id { get; set; }

    [MaxLength(40)] 
    public string? NickName { get; set; }
    
    public Blog? Blog { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}