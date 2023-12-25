namespace PersonalBlog.Common.DTO;

public class BlogDTO
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public string UserNickName { get; set; }
    
    public ICollection<PostDTO>? Posts { get; set; }
}