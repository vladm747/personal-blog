using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalBlog.API;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;

namespace Tests.IntegrationTests.TestSetup;

public class ApplicationFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<PersonalBlogContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            services.AddDbContext<PersonalBlogContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
        
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<PersonalBlogContext>();
            
            ConfigureUsers(db);
            ConfigureBlog(db);
            ConfigurePosts(db);
            ConfigureComments(db);
            
            db.Database.EnsureCreated();
        });
    }
    
    private static void ConfigureUsers(PersonalBlogContext context)
    {
        context.Users.Add(new User
        {
            Id = "f18773fa-495b-424f-87a6-52a9a8d07027",
            Email = "author@gmail.com",
            NickName = "Author",
            PasswordHash = "Password@1"
        });
        context.Users.Add(new User
        {
            Id = "3b64ef62-c0b1-4cce-aea5-75b3f45d2624",
            Email = "user@gmail.com",
            NickName = "User 1",
            PasswordHash = "Password@1"
        });
        
        context.SaveChanges();
    }

    private static void ConfigureRoles(PersonalBlogContext context)
    {
        context.Roles.Add(new IdentityRole("author")
        {
            NormalizedName = "AUTHOR"
        });
        context.Roles.Add(new IdentityRole("user")
        {
            NormalizedName = "USER"
        });
        
        context.SaveChanges();
    }
    
    private static void ConfigureBlog(PersonalBlogContext context)
    {
        context.Blogs.Add(new Blog
        {
            Id = 1,
            UserId = "f18773fa-495b-424f-87a6-52a9a8d07027"
        });

        context.SaveChanges();
    }
    
    private static void ConfigurePosts(PersonalBlogContext context)
    {
        context.Posts.Add(new Post()
        {
            Id = 1,
            UserId = "f18773fa-495b-424f-87a6-52a9a8d07027",
            Content = "Post 1",
            BlogId = 1
        });
        context.Posts.Add(new Post()
        {
            Id = 2,
            UserId = "f18773fa-495b-424f-87a6-52a9a8d07027",
            Content = "Post 1",
            BlogId = 1
        });

        context.SaveChanges();
    }
    
    private static void ConfigureComments(PersonalBlogContext context)
    {
        context.Comments.Add(new Comment()
        {
            Id = 1,
            UserId = "f18773fa-495b-424f-87a6-52a9a8d07027",
            Content = "Comment 1",
            PostId = 1
        });
        context.Comments.Add(new Comment()
        {
            Id = 2,
            UserId = "f18773fa-495b-424f-87a6-52a9a8d07027",
            Content = "Comment 1",
            PostId = 1
        });

        context.SaveChanges();
    }
    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }
}