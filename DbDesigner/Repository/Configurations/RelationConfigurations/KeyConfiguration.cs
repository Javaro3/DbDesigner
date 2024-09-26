using Common.Domain.BaseDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configurations.RelationConfigurations;

public static class KeyConfiguration
{
    public static void KeyConfigure(this ModelBuilder modelBuilder, IEnumerable<Type> modelTypes)
    {
        foreach (var modelType in modelTypes)
        {
            modelBuilder.Entity(modelType, entityBuilder =>
            {
                entityBuilder.HasKey(nameof(IHasId.Id));
            });
        }
    }
}