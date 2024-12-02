using Common.Domain;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class DataBaseTypeConfiguration : IEntityTypeConfiguration<DataBaseType>
{
    public void Configure(EntityTypeBuilder<DataBaseType> builder)
    {
        var databaseTypes = Enum.GetValues<SqlTypeEnum>()
            .SelectMany(sqlType => sqlType.GetDataBases()
                .Select(database => new DataBaseType
                {
                    DataBaseId = Convert.ToInt32(database),
                    SqlTypeId = Convert.ToInt32(sqlType)
                }));

        builder.HasData(databaseTypes);
    }
}