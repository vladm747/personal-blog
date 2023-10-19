using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalBlog.API.Helpers;
using PersonalBlog.BLL.Interfaces;
using PersonalBlog.BLL.Services;
using PersonalBlog.DAL.Context;
using PersonalBlog.DAL.Entities.Auth;

namespace PersonalBlog.API.Startup;

public static partial class ServiceInitializer
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterCustomDependencies(services, configuration);
        RegisterSwagger(services);
        return services;
    }
    public static void RegisterCustomDependencies(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonalBlogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PersonalBlog")));
        
        services.AddScoped<IAuthService, AuthService>();  
        services.AddScoped<IRoleService, RoleService>();  
        
        services.AddCors(options =>
        {
            options.AddPolicy(name: "_myAllowSpecificOrigins",
                policy  =>
                {
                    policy.WithOrigins("http://localhost:5189");
                });
        });
        
        services.AddControllers();
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
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
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