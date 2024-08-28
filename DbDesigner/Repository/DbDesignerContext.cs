using Common.Domain;
using Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Configurations;

namespace Repository;

public class DbDesignerContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    private readonly IOptions<AuthOptions> _authorizationOptions;
    
    public DbDesignerContext(
        DbContextOptions<DbDesignerContext> options, 
        IOptions<AuthOptions> authorizationOptions) : base(options)
    {
        _authorizationOptions = authorizationOptions;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authorizationOptions.Value));
    }
}