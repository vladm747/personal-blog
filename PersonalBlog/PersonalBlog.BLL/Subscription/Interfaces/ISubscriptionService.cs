
using System.Security.Claims;

namespace PersonalBlog.BLL.Subscription.Interfaces;

public interface ISubscriptionService
{
    Task<bool> Subscribe(ClaimsPrincipal userPrincipal, int blogId);
    Task<bool> Unsubscribe(ClaimsPrincipal userPrincipal, int blogId);
    IEnumerable<int> GetSubscriptions(ClaimsPrincipal userPrincipal);
    void Notify(ClaimsPrincipal userPrincipal, int blogId);
}