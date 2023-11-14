using System.Security.Claims;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.BLL.Subscription.Interfaces;
using PersonalBlog.DAL.Entities.Auth;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
    
namespace PersonalBlog.BLL.Subscription.Services;

public class SubscriptionService: ISubscriptionService
{
    private readonly ISubscriptionRepository _repo;
    private readonly IUserService _userService;

    public SubscriptionService(ISubscriptionRepository repo, IUserService userService)
    {
        _repo = repo;
        _userService = userService;
    }

    public IEnumerable<int> GetSubscriptions(ClaimsPrincipal userPrincipal)
    {
        var userId = _userService.GetUserId(userPrincipal);
            
        return _repo.GetSubscriptions(userId);
    }

    public async Task<bool> Subscribe(ClaimsPrincipal userPrincipal, int blogId)
    {
        var userId = _userService.GetUserId(userPrincipal);
        
        return await _repo.AddSubscriptionAsync(new DAL.Entities.Subscription
        {
            BlogId = blogId,
            UserId = userId
        });
    }

    public async Task<bool> Unsubscribe(ClaimsPrincipal userPrincipal, int blogId)
    {
        var userId = _userService.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException();
        var subscription = await _repo.GetSubscriptionAsync(userId, blogId);
        
        if (subscription == null)
            throw new ArgumentNullException("No such subscription found!");

        return await _repo.DeleteSubscriptionAsync(subscription);
    }


    public void Notify(ClaimsPrincipal userPrincipal, int blogId)
    {
        try
        {
            var message = new MimeMessage();
            var ids =  _repo.GetSubscribers(blogId);

            if (!ids.IsNullOrEmpty())
            {
                var nickName =  _userService.GetNickName(userPrincipal);
                var emails =  _userService.GetUsersEmails(ids);
                
                message.From.Add(new MailboxAddress("Arch Bergstrom", "arch.bergstrom@ethereal.email"));
                
                foreach (var email in emails)
                {
                    message.To.Add(MailboxAddress.Parse(email));
                }

                message.Subject = $"The new post of {nickName} just came out!";

                message.Body = new TextPart("plain")
                {
                    Text = "Dear Subscriber, visit our site to check out new updates!"
                };
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("arch.bergstrom@ethereal.email", "wWMydCRVzjuWuTMvTh");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}