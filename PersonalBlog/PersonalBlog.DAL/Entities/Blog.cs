using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.DAL.Entities;

public class Blog
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    
    public User? User { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}