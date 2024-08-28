using Common.Domain;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(e => e.Permissions)
            .WithMany(e => e.Roles)
            .UsingEntity<RolePermission>(
                l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId),
                r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId));

        var roles = Enum.GetValues<RoleEnum>()
            .Select(e => new Role
            {
                Id = (int)e,
                Name = e.ToString()
            });

        builder.HasData(roles);
    }
}