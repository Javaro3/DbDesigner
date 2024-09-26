using System.Linq.Expressions;
using Common.Domain.BaseDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.RelationConfigurations;

public class BaseManyToManyConfiguration<T1, T2, TMany> : IEntityTypeConfiguration<T1>
    where T1 : BaseModel
    where T2 : BaseModel
    where TMany : BaseModel

{
    private readonly Expression<Func<T1, IEnumerable<T2>>> _hasMany1Expression;
    private readonly Expression<Func<T2, IEnumerable<T1>>> _hasMany2Expression;
    private readonly Expression<Func<TMany, object>> _foreignKey1Expression;
    private readonly Expression<Func<TMany, object>> _foreignKey2Expression;

    public BaseManyToManyConfiguration(
        Expression<Func<T1, IEnumerable<T2>>> hasMany1Expression,
        Expression<Func<T2, IEnumerable<T1>>> hasMany2Expression, 
        Expression<Func<TMany, object>> foreignKey1Expression,
        Expression<Func<TMany, object>> foreignKey2Expression)
    {
        _hasMany1Expression = hasMany1Expression;
        _hasMany2Expression = hasMany2Expression;
        _foreignKey1Expression = foreignKey1Expression;
        _foreignKey2Expression = foreignKey2Expression;
    }
    
    public void Configure(EntityTypeBuilder<T1> builder)
    {
        builder.HasMany(_hasMany1Expression!)
            .WithMany(_hasMany2Expression!)
            .UsingEntity<TMany>(
                l => l.HasOne<T2>().WithMany().HasForeignKey(_foreignKey1Expression!),
                r => r.HasOne<T1>().WithMany().HasForeignKey(_foreignKey2Expression!));
    }
}