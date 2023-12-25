using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.BLL.Subscription.Interfaces;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.FluentEmail.Interfaces;

namespace PersonalBlog.BLL.Subscription.Services;

public class SubscriptionService: ISubscriptionService
{
    private readonly ISubscriptionRepository _repo;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public SubscriptionService(ISubscriptionRepository repo, IUserService userService, IEmailService emailService)
    {
        _repo = repo;
        _userService = userService;
        _emailService = emailService;
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


    public void Notify(string nickName, int blogId)
    {
        try
        {
            var ids =  _repo.GetSubscribers(blogId);

            if (!ids.IsNullOrEmpty())
            {
                var emails =  _userService.GetUsersEmails(ids);
                
                _emailService.Send(emails, nickName);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}