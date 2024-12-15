using Common.Domain;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class LanguageTypeSqlTypeConfiguration: IEntityTypeConfiguration<LanguageTypeSqlType>
{
    public void Configure(EntityTypeBuilder<LanguageTypeSqlType> builder)
    {
        var languageTypeSqlTypes = Enum.GetValues<LanguageTypeEnum>()
            .SelectMany(language => language.GetSqlTypes()
                .Select(sqlType => new LanguageTypeSqlType
                {
                    LanguageTypeId = Convert.ToInt32(language),
                    SqlTypeId = Convert.ToInt32(sqlType)
                }));
        
        builder.HasData(languageTypeSqlTypes);
    }
}