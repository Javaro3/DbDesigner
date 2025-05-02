using DbDesigner.Domain.Domain;
using DbDesigner.Domain.Domain.BaseDomain;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Extensions;
using DbDesigner.Infrastructure.Configurations.DataConfigurations;
using DbDesigner.Infrastructure.Configurations.RelationConfigurations;
using Microsoft.EntityFrameworkCore;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Infrastructure;

public static class DbContextExtensions
{
    public static void ConfigureRelations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Index, Column, IndexColumn>(
            e => e.Columns,
            e => e.Indices,
            e => e.ColumnId,
            e => e.IndexId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<User, Role, UserRole>(
            e => e.Roles,
            e => e.Users,
            e => e.RoleId,
            e => e.UserId
        ));
    }

    public static void ConfigureData(this ModelBuilder modelBuilder)
    {
        var generationModelSelector = CreateBaseEnumFieldsSelector<GenerationModelEnum>()
            .AddNameToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<GenerationModel, GenerationModelEnum>(generationModelSelector));
        
        var generationLanguageSelector = CreateBaseEnumFieldsSelector<GenerationLanguageEnum>()
            .AddNameToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<GenerationLanguage, GenerationLanguageEnum>(generationLanguageSelector));
        
        var architectureEnumSelector = CreateBaseEnumFieldsSelector<ArchitectureEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Architecture, ArchitectureEnum>(architectureEnumSelector));

        var dataBaseEnumSelector = CreateBaseEnumFieldsSelector<DataBaseEnum>()
            .AddNameToEnumFieldsSelector()
            .AddImageToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<DataBase, DataBaseEnum>(dataBaseEnumSelector));

        var indexTypeEnumSelector = CreateBaseEnumFieldsSelector<IndexTypeEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddDataBaseToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<IndexType, IndexTypeEnum>(indexTypeEnumSelector));

        var languageEnumSelector = CreateBaseEnumFieldsSelector<LanguageEnum>()
            .AddNameToEnumFieldsSelector()
            .AddImageToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Language, LanguageEnum>(languageEnumSelector));

        var ormEnumSelector = CreateBaseEnumFieldsSelector<OrmEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddLanguageToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Orm, OrmEnum>(ormEnumSelector));

        var propertyEnumSelector = CreateBaseEnumFieldsSelector<PropertyEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddParamsToEnumFieldsSelector()
            .AddDataBaseToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Property, PropertyEnum>(propertyEnumSelector));
        
        var relationActionEnumSelector = CreateBaseEnumFieldsSelector<RelationActionEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<RelationAction, RelationActionEnum>(relationActionEnumSelector));
        
        var sqlTypeEnumSelector = CreateBaseEnumFieldsSelector<SqlTypeEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddParamsToEnumFieldsSelector()
            .AddDataBaseToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<SqlType, SqlTypeEnum>(sqlTypeEnumSelector));
        
        var roleEnumSelector = CreateBaseEnumFieldsSelector<RoleEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Role, RoleEnum>(roleEnumSelector));

        var userConfiguration = new UserConfiguration();
        var userRoleConfiguration = new UserRoleConfiguration(userConfiguration.Users);
        modelBuilder.ApplyConfiguration(userConfiguration);
        modelBuilder.ApplyConfiguration(userRoleConfiguration);
    }
    
    private static Dictionary<string, Func<T, object>> CreateBaseEnumFieldsSelector<T>() where T: Enum
    {
        return new() { { nameof(IHasId.Id), e => Convert.ToInt32(e) } };
    }
    
    private static Dictionary<string, Func<T, object>> AddNameToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(IHasName.Name), e => e.GetName());
        return selector;
    }
    
    private static Dictionary<string, Func<T, object>> AddImageToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(IHasImage.Image), e => e.GetImage());
        return selector;
    }
    
    private static Dictionary<string, Func<T, object>> AddDescriptionToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(IHasDescription.Description), e => e.GetDescription());
        return selector;
    }
    
    private static Dictionary<string, Func<T, object>> AddParamsToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(IHasParams.HasParams), e => e.GetParams());
        return selector;
    }

    private static Dictionary<string, Func<T, object>> AddDataBaseToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(IndexType.DataBaseId), e => Convert.ToInt32(e.GetDataBase()));
        return selector;
    }
    
    private static Dictionary<string, Func<T, object>> AddLanguageToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(Orm.LanguageId), e => Convert.ToInt32(e.GetLanguage()));
        return selector;
    }
}