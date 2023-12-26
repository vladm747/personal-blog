using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations;
using Tests.UnitTests.TestFixtures;

namespace Tests.UnitTests;

public class SubscriptionRepositoryTests: IClassFixture<DatabaseSeedDataFixture>
{
    private readonly DatabaseSeedDataFixture _fixture;
    private readonly ISubscriptionRepository _repository;

    public SubscriptionRepositoryTests(DatabaseSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new SubscriptionRepository(_fixture.Context);
    }
    
    [Fact]
    public void GetSubscribers_ShouldReturn1Subscriber_WhenSubscriberExists()
    {
        // Act
        var result =  _repository.GetSubscribers(2);
        var enumerable = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(enumerable.Count() == 1);
    }
    
    [Fact]
    public void GetSubscriptions_ShouldReturn1Subscription_WhenSubscriptionExists()
    {
        // Act
        var result =  _repository.GetSubscriptions("98ef8882-d443-4593-be9c-3570da9a8f67");
        var enumerable = result.ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(enumerable.Count() == 1);
    }
    
    [Fact]
    public async void GetSubscriptionAsync_ShouldReturn1Subscription_WhenSubscriptionExists()
    {
        // Act
        var result =  await _repository.GetSubscriptionAsync("98ef8882-d443-4593-be9c-3570da9a8f67", 2); 
        // Assert
        Assert.Equal("98ef8882-d443-4593-be9c-3570da9a8f67", result.UserId);
    }
    
    [Fact]
    public async void AddSubscription_ShouldReturnTrue_WhenBlogAndUserExists()
    {
        //Arrange
        var subscription = new Subscription
        {
            UserId = "5b1a9e4e-fc0a-4fcb-8fc0-1768c3157523",
            BlogId = 1
        };
        
        // Act
        var result = await _repository.AddSubscriptionAsync(subscription);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async void DeleteSubscription_ShouldReturnTrue_WhenBlogAndUserExists()
    {
        //Arrange
        var subscription = _repository.GetSubscriptionAsync("98ef8882-d443-4593-be9c-3570da9a8f67", 2).Result;
        
        // Act
        var result = await _repository.DeleteSubscriptionAsync(subscription);
        
        // Assert
        Assert.True(result);
    }
}