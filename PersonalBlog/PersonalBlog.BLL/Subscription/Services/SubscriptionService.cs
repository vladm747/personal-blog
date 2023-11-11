using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.BLL.Subscription.Interfaces;
using PersonalBlog.DAL.Entities.Auth;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
    
namespace PersonalBlog.BLL.Subscription.Services;

public class SubscriptionService: ISubscriptionService
{
    private readonly ISubscriptionRepository _repo;
    private readonly UserManager<User> _userManager;

    public SubscriptionService(ISubscriptionRepository repo, UserManager<User> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    public async Task<IEnumerable<int>> GetSubscriptions(ClaimsPrincipal userPrincipal)
    {
        var userId = _userManager.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException();
            
        return await _repo.GetSubscriptionsAsync(userId);
    }

    public async Task<bool> Subscribe(ClaimsPrincipal userPrincipal, int blogId)
    {
        var userId = _userManager.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException();
        
        return await _repo.AddSubscriptionAsync(new DAL.Entities.Subscription
        {
            BlogId = blogId,
            UserId = userId
        });
    }

    public async Task<bool> Unsubscribe(ClaimsPrincipal userPrincipal, int blogId)
    {
        var userId = _userManager.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException();
        var subscription = await _repo.GetSubscriptionAsync(userId, blogId);
        
        if (subscription == null)
            throw new ArgumentNullException("No such subscription found!");

        return await _repo.DeleteSubscriptionAsync(subscription);
    }


    public async Task Notify(ClaimsPrincipal userPrincipal, int blogId)
    {
        var userId = _userManager.GetUserId(userPrincipal) ?? throw new UnauthorizedAccessException();

    }
}