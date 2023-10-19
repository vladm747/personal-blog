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
        modelBuilder.Entity<Blog>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Blog)
            .HasForeignKey(e => e.BlogId)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasOne(e => e.Blog)
            .WithMany(e => e.Posts)
            .HasForeignKey(e => e.BlogId)
            .IsRequired();
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired();
        
        modelBuilder.Entity<Comment>()
            .HasOne(e => e.Post)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.PostId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=.;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=true");
    
}