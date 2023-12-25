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
        modelBuilder.Entity<IdentityRole>().HasData(new[]
        {
            new IdentityRole("author")
            {
                NormalizedName = "AUTHOR"
            },
            new IdentityRole("user")
            {
                NormalizedName = "USER"
            }
        });

        modelBuilder.Entity<User>()
            .HasOne(e => e.Blog)
            .WithOne(e => e.User)
            .HasForeignKey<Blog>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Blog>()
            .HasMany(item => item.Posts)
            .WithOne(item => item.Blog)
            .HasForeignKey(e => e.BlogId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasMany(item => item.Comments)
            .WithOne(item => item.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<Comment>()
            .HasOne(item => item.User)
            .WithMany(item => item.Comments)
            .HasForeignKey(item => item.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();


        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=.;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=true");
    
}