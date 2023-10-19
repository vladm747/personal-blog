using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.DAL.Context;

public class PersonalBlogContext: IdentityDbContext<User>
{
    public PersonalBlogContext() { }
    
    public PersonalBlogContext(DbContextOptions<PersonalBlogContext> options)
        : base(options)
    {
    }

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
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=PersonalBlogDB;Trusted_Connection=True;TrustServerCertificate=true");
    }
}