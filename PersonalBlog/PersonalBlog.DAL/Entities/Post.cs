using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.DAL.Entities;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    [Required] 
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime PublicationDate { get; set; }
  
    public string UserId { get; set; }
    public User User { get; set; }
    public int BlogId { get; set; }
    public Blog? Blog { get; set; } = null!;
    
    public IEnumerable<Comment>? Comments { get; set; }
}