using Common.Domain;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        var permissions = Enum.GetValues<PermissionEnum>()
            .Select(e => new Permission
            {
                Id = (int)e,
                Name = e.ToString()
            });
        
        builder.HasData(permissions);
    }
}