namespace PersonalBlog.FluentEmail.Exception;

public class EmailServiceException: System.Exception
{
    public EmailServiceException() { }

    public EmailServiceException(string message) : base(message) { }

    public EmailServiceException(string message, System.Exception inner) : base(message, inner) { }
}