using Common.Domain;
using Common.Domain.BaseDomain;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations.DataConfigurations;
using Repository.Configurations.RelationConfigurations;
using Index = Common.Domain.Index;

namespace Repository;

public static class DbContextExtensions
{
    public static void ConfigureRelations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Column, Property, ColumnProperty>(
            e => e.Properties,
            e => e.Columns,
            e => e.PropertyId,
            e => e.ColumnId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<DataBase, IndexType, DataBaseIndexType>(
            e => e.IndexTypes,
            e => e.DataBases,
            e => e.IndexTypeId,
            e => e.DataBaseId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<DataBase, SqlType, DataBaseType>(
            e => e.SqlTypes,
            e => e.DataBases,
            e => e.SqlTypeId,
            e => e.DataBaseId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Index, Column, IndexColumn>(
            e => e.Columns,
            e => e.Indices,
            e => e.ColumnId,
            e => e.IndexId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Language, Orm, LanguageOrm>(
            e => e.Orms,
            e => e.Languages,
            e => e.OrmId,
            e => e.LanguageId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Project, Table, ProjectTable>(
            e => e.Tables,
            e => e.Projects,
            e => e.TableId,
            e => e.ProjectId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<Table, Column, TableColumn>(
            e => e.Columns,
            e => e.Tables,
            e => e.ColumnId,
            e => e.TableId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<User, Project, UserProject>(
            e => e.Projects,
            e => e.Users,
            e => e.ProjectId,
            e => e.UserId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<User, Role, UserRole>(
            e => e.Roles,
            e => e.Users,
            e => e.RoleId,
            e => e.UserId
        ));
        
        modelBuilder.ApplyConfiguration(new BaseManyToManyConfiguration<LanguageType, SqlType, LanguageTypeSqlType>(
            e => e.SqlTypes,
            e => e.LanguageTypes,
            e => e.SqlTypeId,
            e => e.LanguageTypeId
        ));
    }

    public static void ConfigureData(this ModelBuilder modelBuilder)
    {
        var roleEnumSelector = CreateBaseEnumFieldsSelector<RoleEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Role, RoleEnum>(roleEnumSelector));
        
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
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<IndexType, IndexTypeEnum>(indexTypeEnumSelector));
        
        var languageEnumSelector = CreateBaseEnumFieldsSelector<LanguageEnum>()
            .AddNameToEnumFieldsSelector()
            .AddImageToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Language, LanguageEnum>(languageEnumSelector));
        
        var ormEnumSelector = CreateBaseEnumFieldsSelector<OrmEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Orm, OrmEnum>(ormEnumSelector));
        
        var relationActionEnumSelector = CreateBaseEnumFieldsSelector<RelationActionEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<RelationAction, RelationActionEnum>(relationActionEnumSelector));
        
        var propertyEnumSelector = CreateBaseEnumFieldsSelector<PropertyEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddParamsToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<Property, PropertyEnum>(propertyEnumSelector));

        var sqlTypeEnumSelector = CreateBaseEnumFieldsSelector<SqlTypeEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddParamsToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<SqlType, SqlTypeEnum>(sqlTypeEnumSelector));

        var languageTypeEnumSelector = CreateBaseEnumFieldsSelector<LanguageTypeEnum>()
            .AddNameToEnumFieldsSelector()
            .AddDescriptionToEnumFieldsSelector()
            .AddLanguageToEnumFieldsSelector();
        modelBuilder.ApplyConfiguration(new BaseEnumConfiguration<LanguageType, LanguageTypeEnum>(languageTypeEnumSelector));

        
        modelBuilder.ApplyConfiguration(new LanguageOrmConfiguration());
        modelBuilder.ApplyConfiguration(new DataBaseIndexTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DataBaseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageTypeSqlTypeConfiguration());


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

    private static Dictionary<string, Func<T, object>> AddLanguageToEnumFieldsSelector<T>(this Dictionary<string, Func<T, object>> selector) where T: Enum
    {
        selector.Add(nameof(LanguageType.LanguageId), e => e.GetLanguages().First());
        return selector;
    }
}