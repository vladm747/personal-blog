namespace PersonalBlog.BLL.DTO;

public class CommentDTO
{
    public int Id { get; set; }
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CommentDate { get; set; }

    public string UserId { get; set; }
    
    public int PostId { get; set; }
}