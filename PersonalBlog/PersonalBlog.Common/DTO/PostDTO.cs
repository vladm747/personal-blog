namespace PersonalBlog.BLL.DTO;

public class PostDTO
{
    public int Id { get; set; }
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime PublicationDate { get; set; }
    
    public string UserId { get; set; }
    public string UserNickName { get; set; }
    
    public int BlogId { get; set; }
}