namespace PersonalBlog.FluentEmail.Interfaces;

public interface IEmailService
{
    Task<bool> Send(IEnumerable<string> emails, string nickName);
}