using PersonalBlog.DAL.Entities;

namespace PersonalBlog.DAL.Infrastructure.DI.Abstract;

public interface ISubscriptionRepository
{
    Task<Subscription> GetSubscriptionAsync(string userId, int blogId);
    Task<IEnumerable<int>> GetSubscriptionsAsync(string userId);
    Task<bool> AddSubscriptionAsync(Subscription subscription);
    Task<bool> DeleteSubscriptionAsync(Subscription subscription);
}