using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalBlog.API.Filters;
using PersonalBlog.BLL.Helpers;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.BLL.Interfaces.Auth;
using PersonalBlog.BLL.Profiles;
using PersonalBlog.BLL.Services;
using PersonalBlog.BLL.Services.Auth;
using PersonalBlog.BLL.Subscription.Interfaces;
using PersonalBlog.BLL.Subscription.Services;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities.Auth;
using PersonalBlog.DAL.Infrastructure.DI.Abstract;
using PersonalBlog.DAL.Infrastructure.DI.Implementations;

namespace PersonalBlog.API.Startup;

public static class ServiceInitializer
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterCustomDependencies(services, configuration);
        RegisterSwagger(services);
        return services;
    }
    public static void RegisterCustomDependencies(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        services.AddDbContext<PersonalBlogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PersonalBlog")));
        
        services.AddScoped<DbContext, PersonalBlogContext>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowAllOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
        });
        
        services.AddControllers(options => options.Filters.Add<PersonalBlogExceptionFilterAttribute>());
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<PersonalBlogContext>()
            .AddDefaultTokenProviders();
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(1));

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        services
            .AddAuthorization(options => 
                options.AddPolicy("ElevatedRights", policy =>
                policy.RequireRole("author", "user")))
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options => {
                options.LoginPath = "/Auth/login/";
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

    private static void RegisterSwagger(IServiceCollection services)
    {
        //services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT containing userid claim",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });
            var security =
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            UnresolvedReference = true
                        },
                        new List<string>()
                    }
                };
            c.AddSecurityRequirement(security);
        });
    }
}