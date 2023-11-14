using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.Infrastructure.DI.Abstract;

public interface ISubscriptionRepository
{
    Task<Subscription> GetSubscriptionAsync(string userId, int blogId);
    IEnumerable<string> GetSubscribers(int blogId);
    IEnumerable<int> GetSubscriptions(string userId);
    Task<bool> AddSubscriptionAsync(Subscription subscription);
    Task<bool> DeleteSubscriptionAsync(Subscription subscription);
}