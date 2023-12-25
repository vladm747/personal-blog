using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;

namespace PersonalBlog.DAL.Infrastructure.DI.Implementations;

public class SubscriptionRepository: ISubscriptionRepository
{
    private readonly PersonalBlogContext _database;
    private ISubscriptionRepository _subscriptionRepositoryImplementation;

    public SubscriptionRepository(PersonalBlogContext database) =>
        _database = database;


    public IEnumerable<string> GetSubscribers(int blogId) =>
        _database.Subscriptions.Where(subs => subs.BlogId == blogId)
            .Select(item => item.UserId).AsNoTracking().ToList();


    public IEnumerable<int> GetSubscriptions(string userId) => _database.Subscriptions
            .Where(item => item.UserId == userId)
            .Select(item => item.BlogId).ToList();

    public async Task<Subscription> GetSubscriptionAsync(string userId, int blogId) =>
        await _database.Subscriptions.Where(subscription =>
            subscription.UserId == userId && subscription.BlogId == blogId).FirstOrDefaultAsync(); 

    public async Task<bool> AddSubscriptionAsync(Subscription subscription)
    {
        await _database.Subscriptions.AddAsync(subscription);
        var result = await _database.SaveChangesAsync();
        return result != 0 ? true : false;
    }

    public async Task<bool> DeleteSubscriptionAsync(Subscription subscription)
    {
        _database.Subscriptions.Remove(subscription);
        var result = await _database.SaveChangesAsync();
        return result != 0 ? true : false;
    }
}