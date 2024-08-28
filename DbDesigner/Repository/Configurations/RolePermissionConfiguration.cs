using Common.Domain;
using Common.Enums;
using Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    private readonly AuthOptions _authOptions;

    public RolePermissionConfiguration(AuthOptions authOptions)
    {
        _authOptions = authOptions;
    }
    
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        var rolePermissions = GetRolePermissions();
        builder.HasData(rolePermissions);
    }

    private IEnumerable<RolePermission> GetRolePermissions()
    {
        var id = 1;
        return _authOptions.RolePermissions
            .SelectMany(e => e.Permissions
                .Select(i => new RolePermission {
                    Id = id++,
                    RoleId = (int)Enum.Parse<RoleEnum>(e.Role),
                    PermissionId = (int)Enum.Parse<PermissionEnum>(i)}));
    }
}