using System.Text;
using Common.Domain;
using Common.Options;
using Common.Profiles;
using Infrastructure;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service;
using Service.DataServicies;
using Service.Interfaces;
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
        services.AddScoped<UserService>();
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
    
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOption = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
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
            });

        services.AddAuthorization();
    }
}