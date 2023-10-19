using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.DAL.Entities;

public class Blog
{
    [Key]
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}