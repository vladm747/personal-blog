using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Options;
using PersonalBlog.FluentEmail.Interfaces;
using PersonalBlog.Common.Models;
using PersonalBlog.FluentEmail.Exception;

namespace PersonalBlog.FluentEmail.Services;

public class EmailService: IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly MessageSettings _messageSettings;

    public EmailService(IFluentEmail fluentEmail, IOptions<MessageSettings> messageSettings)
    {
        _fluentEmail = fluentEmail;
        _messageSettings = messageSettings.Value;
    }

    public async Task<bool> Send(IEnumerable<string> emails, string nickName)
    {
        try
        {
            SendResponse? sendEmail = null;
        
            foreach (var email in emails)
            {
                sendEmail = await _fluentEmail
                    .To(email)
                    .Subject("New Updates")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}.FluentEmail//{_messageSettings.MessagePath}", new{ Email = email, NickName = nickName })
                    .SendAsync();
            }

            _fluentEmail.Data.ToAddresses.Clear();

            return sendEmail!.Successful;
        }
        catch (System.Exception e)
        {
            throw new EmailServiceException(e.Message);
        }
    }
}