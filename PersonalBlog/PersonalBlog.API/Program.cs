using Microsoft.AspNetCore.Identity;
using PersonalBlog.API.Startup;
namespace PersonalBlog.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.RegisterApplicationServices(builder.Configuration);

        var app = builder.Build();
        app.ConfigureMiddleware();
        
        await using var scope = app.Services.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await CreateRolesAsync(roleManager);
        
        await app.RunAsync();
    }
    
    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("author"))
            await roleManager.CreateAsync(new IdentityRole("author"));

        if (!await roleManager.RoleExistsAsync("user"))
            await roleManager.CreateAsync(new IdentityRole("user"));
    }
}