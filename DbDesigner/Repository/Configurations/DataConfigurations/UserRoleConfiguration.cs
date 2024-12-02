using Common.Domain;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    private readonly List<User> _users;
    
    public UserRoleConfiguration(List<User> users)
    {
        _users = users;
    }
    
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var admin = _users.FirstOrDefault(u => u.Email == "admin@gmail.com");
        if (admin != null)
            builder.HasData(CreateRolesForUser(admin.Id, RoleEnum.Admin, RoleEnum.User));

        var user = _users.FirstOrDefault(u => u.Email == "user@gmail.com");
        if (user != null)
            builder.HasData(CreateRolesForUser(user.Id, RoleEnum.User));
    }

    private static List<UserRole> CreateRolesForUser(int userId, params RoleEnum[] roles)
    {
        return roles.Select(role => new UserRole
        {
            UserId = userId,
            RoleId = Convert.ToInt32(role)
        }).ToList();
    }
}