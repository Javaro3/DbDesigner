using Common.Domain;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class DataBaseIndexTypeConfiguration: IEntityTypeConfiguration<DataBaseIndexType>
{
    public void Configure(EntityTypeBuilder<DataBaseIndexType> builder)
    {
        var databaseIndexTypes = Enum.GetValues<IndexTypeEnum>()
            .SelectMany(index => index.GetDataBases()
                .Select(database => new DataBaseIndexType
                {
                    DataBaseId = Convert.ToInt32(database),
                    IndexTypeId = Convert.ToInt32(index)
                }));
        
        builder.HasData(databaseIndexTypes);
    }
}