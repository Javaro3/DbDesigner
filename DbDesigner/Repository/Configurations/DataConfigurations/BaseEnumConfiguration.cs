using Common.Domain.BaseDomain;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.DataConfigurations;

public class BaseEnumConfiguration<TModel, TEnum> : IEntityTypeConfiguration<TModel>
    where TModel : BaseModel, IHasId, IHasName, IHasDescription, new()
    where TEnum : Enum
{
    private readonly Dictionary<string, Func<TEnum, object>> _fields;

    public BaseEnumConfiguration(Dictionary<string, Func<TEnum, object>> fields)
    {
        _fields = fields;
    }
    
    public void Configure(EntityTypeBuilder<TModel> builder)
    {
        var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
            .Select(e =>
            {
                var model = new TModel();
                
                foreach (var field in _fields)
                {
                    var property = typeof(TModel).GetProperty(field.Key);
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(model, field.Value(e));
                    }
                }

                return model;
            });

        builder.HasData(values);
    }
}