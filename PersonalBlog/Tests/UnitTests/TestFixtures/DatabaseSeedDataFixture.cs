
using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;

namespace Tests.UnitTests.TestFixtures;

public class DatabaseSeedDataFixture: IDisposable
{
    public PersonalBlogContext Context { get; } = new(
        new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase($"PersonalBlog{Guid.NewGuid()}")
            .Options);
    
    public DatabaseSeedDataFixture()
    {
        Context.Users.AddRange(new List<User>
        {
            new()
            {
                Id = "98ef8882-d443-4593-be9c-3570da9a8f67"
                
            },
            new()
            {
                Id = "5b1a9e4e-fc0a-4fcb-8fc0-1768c3157523"
            }
        });
        Context.Blogs.AddRange(new List<Blog>
        {
            new()
            {
                Id = 1,
                UserId = "98ef8882-d443-4593-be9c-3570da9a8f67"
            },
            new()
            {
                Id = 2,
                UserId = "5b1a9e4e-fc0a-4fcb-8fc0-1768c3157523"
            }
        });
        Context.Posts.AddRange(new List<Post>
        {
            new()
            {
                Id = 1,
                UserId = "98ef8882-d443-4593-be9c-3570da9a8f67",
                BlogId = 1
            },
            new()
            {
                Id = 2,
                UserId = "5b1a9e4e-fc0a-4fcb-8fc0-1768c3157523",
                BlogId = 2
            }
        });
        Context.Comments.AddRange(new List<Comment>
        {
            new()
            {
                Id = 1,
                UserId = "98ef8882-d443-4593-be9c-3570da9a8f67",
                PostId = 1
            },
            new()
            {
                Id = 2,
                UserId = "5b1a9e4e-fc0a-4fcb-8fc0-1768c3157523",
                PostId = 2
            }
        });
        Context.Subscriptions.Add(new Subscription
        {
            Id = 1,
            UserId = "98ef8882-d443-4593-be9c-3570da9a8f67",
            BlogId = 2
        });
        Context.SaveChanges();
    }
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}