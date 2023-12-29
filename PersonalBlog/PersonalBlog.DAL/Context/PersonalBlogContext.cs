using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.DAL.Entities;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.DAL.Context;

public class PersonalBlogContext: IdentityDbContext<User>
{
    public PersonalBlogContext() { }
    
    public PersonalBlogContext(DbContextOptions<PersonalBlogContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(e => e.Blog)
            .WithOne(e => e.User)
            .HasForeignKey<Blog>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.User)
            .HasForeignKey( e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Blog>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Blog)
            .HasForeignKey(e => e.BlogId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }
}