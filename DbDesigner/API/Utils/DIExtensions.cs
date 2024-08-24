using System.Text;
using Common.Domain;
using Common.Options;
using Common.Profiles;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
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
    }
    
    public static void AddServicies(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
    }
    
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
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
        
        services.AddAuthorization();
    }
}