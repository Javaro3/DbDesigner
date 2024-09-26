using System.Text;
using Common.Constants;
using Common.Domain;
using Common.Enums;
using Common.Options;
using Common.Profiles;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DataServicies;
using Service.Interfaces.Infrastructure.Auth;

namespace API.Utils;

public static class DIExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")!;
        services.AddDbContext<DbDesignerContext>(e => e.UseNpgsql(connectionString));
        services.AddScoped<IRepository<User>, UserRespository>();
        services.AddScoped<IRepository<Role>, RoleRepository>();
        services.AddScoped<IRepository<Permission>, PermissionRespository>();
    }
    
    public static void AddServicies(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
    }
    
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<GoogleKeysOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));
    }
    
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
    }
    
    public static void AddGoogleAndJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOption = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        var googleKeysOptions = configuration.GetSection("GoogleKeysOptions").Get<GoogleKeysOptions>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["jwt"];
                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = googleKeysOptions.ClientId;
                options.ClientSecret = googleKeysOptions.ClientSecret;
                options.CallbackPath = googleKeysOptions.CallbackPath;
            });
        
        services.AddAuthorization(e =>
        {
            e.AddCustomPolicy(Policy.Test.TestGet, PermissionEnum.Read);
            e.AddCustomPolicy(Policy.Auth.Logout, PermissionEnum.Read);
        });
    }

    private static void AddCustomPolicy(
        this AuthorizationOptions authorizationOptions, 
        string policyName,
        params PermissionEnum[] permissions)
    {
        
        authorizationOptions.AddPolicy(policyName, c =>
        {
            c.AuthenticationSchemes = [JwtBearerDefaults.AuthenticationScheme];
            c.AddRequirements(new PermissionRequirement(permissions));
        });
    }
}