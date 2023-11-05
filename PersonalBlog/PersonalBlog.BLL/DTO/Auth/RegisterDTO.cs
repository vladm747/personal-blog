namespace PersonalBlog.BLL.DTO.Auth;

public class RegisterDTO
{
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string NickName { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}