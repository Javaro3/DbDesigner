using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Index = Common.Domain.Index;

namespace Repository;

public class DbDesignerContext : DbContext
{
    #region DataSets

    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Role> Roles { get; set; }
    
    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    public virtual DbSet<Architecture> Architectures { get; set; }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<ColumnProperty> ColumnProperties { get; set; }

    public virtual DbSet<DataBase> DataBases { get; set; }

    public virtual DbSet<DataBaseType> DataBaseTypes { get; set; }
    
    public virtual DbSet<Index> Indices { get; set; }

    public virtual DbSet<IndexColumn> IndexColumns { get; set; }

    public virtual DbSet<IndexType> IndexTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }
    
    public virtual DbSet<LanguageOrm> LanguageOrms { get; set; }

    public virtual DbSet<Orm> Orms { get; set; }
    
    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectTable> ProjectTables { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Relation> Relations { get; set; }

    public virtual DbSet<RelationAction> RelationActions { get; set; }

    public virtual DbSet<Table> Tables { get; set; }
    
    public virtual DbSet<TableColumn> TableColumns { get; set; }
    
    public virtual DbSet<SqlType> SqlTypes { get; set; }
    
    public virtual DbSet<LanguageTypeSqlType> LanguageTypeSqlTypes { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }
    
    public virtual DbSet<DataBaseIndexType> DataBaseIndexTypes { get; set; }
    
    public virtual DbSet<LanguageType> LanguageTypes { get; set; }

    #endregion
    
    public DbDesignerContext(
        DbContextOptions<DbDesignerContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureRelations();
        modelBuilder.ConfigureData();
    }
}