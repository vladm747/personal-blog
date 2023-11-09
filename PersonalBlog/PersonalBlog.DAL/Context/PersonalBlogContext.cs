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

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
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
            .HasOne(item => item.Blog)
            .WithOne(item => item.User)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
        
        modelBuilder.Entity<Blog>()
            .HasOne(item => item.User)
            .WithOne(item => item.Blog)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        modelBuilder.Entity<Blog>()
            .HasMany(item => item.Posts)
            .WithOne(item => item.Blog)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasOne(item => item.User)
            .WithMany(item => item.Posts)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        modelBuilder.Entity<Post>()
            .HasMany(item => item.Comments)
            .WithOne(item => item.Post)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
        
        modelBuilder.Entity<Comment>()
            .HasOne(item => item.User)
            .WithMany(item => item.Comments)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();


        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=.;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=true");
    
}