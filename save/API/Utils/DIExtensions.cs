using System.Security.Claims;
using System.Text;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Architecture;
using Common.Dtos.DataBase;
using Common.Dtos.IndexType;
using Common.Dtos.Language;
using Common.Dtos.LanguageType;
using Common.Dtos.Orm;
using Common.Dtos.Project;
using Common.Dtos.Property;
using Common.Dtos.Relation;
using Common.Dtos.RelationAction;
using Common.Dtos.Role;
using Common.Dtos.SqlType;
using Common.Dtos.User;
using Common.Options;
using Common.Profiles;
using Infrastructure.Auth;
using Infrastructure.Generator.Factories;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.CompilerServices;
using Repository;
using Repository.Repositories.Implementations;
using Repository.Repositories.Interfaces;
using Service.DataServices;
using Service.Interfaces.Infrastructure;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace API.Utils;

public static class DIExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")!;
        services.AddDbContext<DbDesignerContext>(e => e.UseNpgsql(connectionString));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRepository<Role>, RoleRepository>();
        services.AddScoped<IRepository<Architecture>, ArchitectureRepository>();
        services.AddScoped<IRepository<DataBase>, DataBaseRepository>();
        services.AddScoped<IRepository<Language>, LanguageRepository>();
        services.AddScoped<IRepository<RelationAction>, RelationActionRepository>();
        services.AddScoped<IRepository<IndexType>, IndexTypeRepository>();
        services.AddScoped<IRepository<Orm>, OrmRepository>();
        services.AddScoped<IRepository<Property>, PropertyRepository>();
        services.AddScoped<IRepository<LanguageType>, LanguageTypeRepository>();
        services.AddScoped<IRepository<SqlType>, SqlTypeRepository>();
        services.AddScoped<IRepository<IndexColumn>, IndexColumnRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IColumnPropertyRepository, ColumnPropertyRepository>();
        services.AddScoped<IIndexRepository, IndexRepository>();
        services.AddScoped<IRelationRepository, RelationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthDataService, AuthDataService>();
        services.AddScoped<IUserDataService, UserDataService>();
        services.AddScoped<IImageDataService, ImageDataService>();
        services.AddScoped<IProjectDataService, ProjectDataService>();
        
        services.AddScoped<IBaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto>, ArchitectureDataService>();
        services.AddScoped<IBaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto>, DataBaseDataService>();
        services.AddScoped<IBaseDataService<Language, LanguageDto, LanguageFilterDto, ComboboxDto>, LanguageDataService>();
        services.AddScoped<IBaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto>, RoleDataService>();
        services.AddScoped<IBaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto>, RelationActionDataService>();
        services.AddScoped<IBaseDataService<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>, IndexTypeDataService>();
        services.AddScoped<IOrmDataService, OrmDataService>();
        services.AddScoped<IBaseDataService<Property, PropertyDto, PropertyFilterDto, HasParamsComboboxDto>, PropertyDataService>();
        services.AddScoped<IBaseDataService<LanguageType, LanguageTypeDto, LanguageTypeFilterDto, ComboboxDto>, LanguageTypeDataService>();
        services.AddScoped<IRelationDataService, RelationDataService>();
        services.AddScoped<ISqlTypeDataService, SqlTypeDataService>();
        services.AddScoped<IIndexTypeDataService, IndexTypeDataService>();
        services.AddScoped<IProjectDataService, ProjectDataService>();
        services.AddScoped<ITableDataService, TableDataService>();
        services.AddScoped<IColumnDataService, ColumnDataService>();
        services.AddScoped<IIndexDataService, IndexDataService>();
        services.AddScoped<IColumnPropertyDataService, ColumnPropertyDataService>();
    }
    
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IDataSourceHelper, DataSourceHelper>();
        services.AddScoped<IBaseHelper<Architecture, ArchitectureFilterDto>, ArchitectureHelper>();
        services.AddScoped<IBaseHelper<DataBase, DataBaseFilterDto>, DataBaseHelper>();
        services.AddScoped<IBaseHelper<Language, LanguageFilterDto>, LanguageHelper>();
        services.AddScoped<IBaseHelper<Role, RoleFilterDto>, RoleHelper>();
        services.AddScoped<IBaseHelper<RelationAction, RelationActionFilterDto>, RelationActionHelper>();
        services.AddScoped<IBaseHelper<IndexType, IndexTypeFilterDto>, IndexTypeHelper>();
        services.AddScoped<IBaseHelper<Orm, OrmFilterDto>, OrmHelper>();
        services.AddScoped<IBaseHelper<Property, PropertyFilterDto>, PropertyHelper>();
        services.AddScoped<IBaseHelper<User, UserFilterDto>, UserHelper>();
        services.AddScoped<IBaseHelper<LanguageType, LanguageTypeFilterDto>, LanguageTypeHelper>();
        services.AddScoped<IBaseHelper<SqlType, SqlTypeFilterDto>, SqlTypeHelper>();
        services.AddScoped<IBaseHelper<Project, ProjectFilterDto>, ProjectHelper>();
        services.AddScoped<IFactory<IDataBaseBuilder>, DataBaseBuilderFactory>();
        services.AddScoped<IFactory<IDomainBuilder>, DomainBuilderFactory>();
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<GoogleKeysOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<FrontendOptions>(configuration.GetSection(nameof(FrontendOptions)));
        services.Configure<ImageOptions>(configuration.GetSection(nameof(ImageOptions)));
        services.Configure<ProjectStorageOptions>(configuration.GetSection(nameof(ProjectStorageOptions)));
    }
    
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(ArchitectureProfile));
        services.AddAutoMapper(typeof(DataBaseProfile));
        services.AddAutoMapper(typeof(LanguageProfile));
        services.AddAutoMapper(typeof(RoleProfile));
        services.AddAutoMapper(typeof(RelationActionProfile));
        services.AddAutoMapper(typeof(IndexTypeProfile));
        services.AddAutoMapper(typeof(OrmProfile));
        services.AddAutoMapper(typeof(PropertyProfile));
        services.AddAutoMapper(typeof(LanguageTypeProfile));
        services.AddAutoMapper(typeof(SqlTypeProfile));
        services.AddAutoMapper(typeof(ProjectProfile));
        services.AddAutoMapper(typeof(ColumnPropertyProfile));
        services.AddAutoMapper(typeof(TableProfile));
        services.AddAutoMapper(typeof(ColumnProfile));
        services.AddAutoMapper(typeof(IndexProfile));
        services.AddAutoMapper(typeof(RelationProfile));
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
                    RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption!.SecretKey))
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers.Authorization.ToString();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                        {
                            context.Token = authHeader["Bearer ".Length..].Trim();
                        }
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