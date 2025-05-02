using System.Security.Claims;
using System.Text;
using DbDesigner.Application.Auth;
using DbDesigner.Application.DataServices;
using DbDesigner.Application.DockerServices;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Architecture;
using DbDesigner.Application.Dtos.Column;
using DbDesigner.Application.Dtos.ColumnProperty;
using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Dtos.GenerationLanguage;
using DbDesigner.Application.Dtos.GenerationModel;
using DbDesigner.Application.Dtos.Index;
using DbDesigner.Application.Dtos.IndexType;
using DbDesigner.Application.Dtos.Language;
using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Application.Dtos.Project;
using DbDesigner.Application.Dtos.Property;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Application.Dtos.RelationAction;
using DbDesigner.Application.Dtos.Role;
using DbDesigner.Application.Dtos.SqlType;
using DbDesigner.Application.Dtos.Table;
using DbDesigner.Application.Dtos.User;
using DbDesigner.Application.Generators;
using DbDesigner.Application.Helpers;
using DbDesigner.Application.Interfaces;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.DockerServices;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Application.Profiles;
using DbDesigner.Domain.Domain;
using DbDesigner.Domain.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DbDesigner.Infrastructure;
using DbDesigner.Infrastructure.Repositories.Implementations;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Web.Utils;

public static class DiExtensions
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
        services.AddScoped<IRepository<SqlType>, SqlTypeRepository>();
        services.AddScoped<IRepository<IndexColumn>, IndexColumnRepository>();
        services.AddScoped<IRepository<Project>, ProjectRepository>();
        services.AddScoped<IRepository<ColumnProperty>, ColumnPropertyRepository>();
        services.AddScoped<IRepository<Index>, IndexRepository>();
        services.AddScoped<IRepository<Relation>, RelationRepository>();
        services.AddScoped<IRepository<Table>, TableRepository>();
        services.AddScoped<IRepository<Column>, ColumnRepository>();
        services.AddScoped<IRepository<GenerationModel>, GenerationModelRepository>();
        services.AddScoped<IRepository<GenerationLanguage>, GenerationLanguageRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthDataService, AuthDataService>();
        services.AddScoped<IUserDataService, UserDataService>();
        services.AddScoped<IImageDataService, ImageDataService>();
        
        services.AddScoped<IBaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto>, ArchitectureDataService>();
        services.AddScoped<IBaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto>, DataBaseDataService>();
        services.AddScoped<IBaseDataService<Language, LanguageDto, LanguageFilterDto, ComboboxDto>, LanguageDataService>();
        services.AddScoped<IBaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto>, RoleDataService>();
        services.AddScoped<IBaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto>, RelationActionDataService>();
        services.AddScoped<IBaseDataService<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>, IndexTypeDataService>();
        services.AddScoped<IOrmDataService, OrmDataService>();
        services.AddScoped<IPropertyDataService, PropertyDataService>();
        services.AddScoped<IBaseDataService<Relation, RelationDto, FilterRequestDto, ComboboxDto>, RelationDataService>();
        services.AddScoped<ISqlTypeDataService, SqlTypeDataService>();
        services.AddScoped<IIndexTypeDataService, IndexTypeDataService>();
        services.AddScoped<IProjectDataService, ProjectDataService>();
        services.AddScoped<IBaseDataService<Table, TableDto, FilterRequestDto, ComboboxDto>, TableDataService>();
        services.AddScoped<IBaseDataService<Column, ColumnDto, FilterRequestDto, ComboboxDto>, ColumnDataService>();
        services.AddScoped<IBaseDataService<Index, IndexDto, FilterRequestDto, ComboboxDto>, IndexDataService>();
        services.AddScoped<IBaseDataService<ColumnProperty, ColumnPropertyDto, FilterRequestDto, ComboboxDto>, ColumnPropertyDataService>();
        services.AddScoped<IBaseDataService<GenerationLanguage, GenerationLanguageDto, GenerationLanguageFilterDto, ComboboxDto>, GenerationLanguageDataService>();
        services.AddScoped<IBaseDataService<GenerationModel, GenerationModelDto, GenerationModelFilterDto, ComboboxDto>, GenerationModelDataService>();
        
        services.AddScoped<ISqlScriptGenerator, SqlScriptGenerator>();
        services.AddScoped<IDalGeneratorManager, DalGeneratorManager>();
        services.AddScoped<ISqlScriptValidator, SqlScriptValidator>();
        services.AddScoped<IProjectStorageManager, ProjectStorageManager>();
        services.AddScoped<IDataGenerator, DataGenerator>();
        
        services.AddScoped<IDockerManager, DockerManager>();
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
        services.AddScoped<IBaseHelper<SqlType, SqlTypeFilterDto>, SqlTypeHelper>();
        services.AddScoped<IBaseHelper<Project, ProjectFilterDto>, ProjectHelper>();
        services.AddScoped<IBaseHelper<GenerationLanguage, GenerationLanguageFilterDto>, GenerationLanguageHelper>();
        services.AddScoped<IBaseHelper<GenerationModel, GenerationModelFilterDto>, GenerationModelHelper>();
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<GoogleKeysOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<FrontendOptions>(configuration.GetSection(nameof(FrontendOptions)));
        services.Configure<ImageOptions>(configuration.GetSection(nameof(ImageOptions)));
        services.Configure<ProjectStorageOptions>(configuration.GetSection(nameof(ProjectStorageOptions)));
        services.Configure<SqlGeneratorOptions>(configuration.GetSection(nameof(SqlGeneratorOptions)));
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
        services.Configure<TemplateStorageOptions>(configuration.GetSection(nameof(TemplateStorageOptions)));
        services.Configure<DataGeneratorServiceOptions>(configuration.GetSection(nameof(DataGeneratorServiceOptions)));
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
        services.AddAutoMapper(typeof(SqlTypeProfile));
        services.AddAutoMapper(typeof(ProjectProfile));
        services.AddAutoMapper(typeof(ColumnPropertyProfile));
        services.AddAutoMapper(typeof(TableProfile));
        services.AddAutoMapper(typeof(ColumnProfile));
        services.AddAutoMapper(typeof(IndexProfile));
        services.AddAutoMapper(typeof(RelationProfile));
        services.AddAutoMapper(typeof(GenerationLanguageProfile));
        services.AddAutoMapper(typeof(GenerationModelProfile));
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