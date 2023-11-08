using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.DAL.Entities;

public class Comment
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime CommentDate { get; set; }
    
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    
    public User User { get; set; }
    
    [ForeignKey(nameof(Post))]
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}