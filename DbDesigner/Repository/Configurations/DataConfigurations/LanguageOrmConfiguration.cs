using Common.Domain;
using Common.Enums;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class LanguageOrmConfiguration : IEntityTypeConfiguration<LanguageOrm>
{
    public void Configure(EntityTypeBuilder<LanguageOrm> builder)
    {
        var languageOrms = Enum.GetValues<OrmEnum>()
            .SelectMany(orm => orm.GetLanguages()
                .Select(lang => new LanguageOrm
                {
                    LanguageId = Convert.ToInt32(lang),
                    OrmId = Convert.ToInt32(orm)
                }));
        
        builder.HasData(languageOrms);
    }
}