using Common.Domain;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public List<User> Users { get; } =
    [
        new()
        {
            Id = 1,
            Name = "admin",
            Email = "admin@gmail.com",
            CreatedOn = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Admin1488")    
        },
        new()
        {
            Id = 2,
            Name = "user",
            Email = "user@gmail.com",
            CreatedOn = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("User1488")
        }
    ];
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(Users);
    }
}