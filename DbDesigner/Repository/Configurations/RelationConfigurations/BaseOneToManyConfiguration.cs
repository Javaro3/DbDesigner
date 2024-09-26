using System.Linq.Expressions;
using Common.Domain.BaseDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations.RelationConfigurations;

public class BaseOneToManyConfiguration<TOne, TMany> : IEntityTypeConfiguration<TOne>
    where TOne : BaseModel
    where TMany : BaseModel
{
    private readonly Expression<Func<TOne, IEnumerable<TMany>>> _hasManyExpression;
    private readonly Expression<Func<TMany, TOne>> _hasOneExpression;
    private readonly Expression<Func<TMany, object>> _foreignKeyExpression;

    public BaseOneToManyConfiguration(
        Expression<Func<TOne, IEnumerable<TMany>>> hasManyExpression,
        Expression<Func<TMany, TOne>> hasOneExpression, 
        Expression<Func<TMany, object>> foreignKeyExpression)
    {
        _hasManyExpression = hasManyExpression;
        _hasOneExpression = hasOneExpression;
        _foreignKeyExpression = foreignKeyExpression;
    }

    public void Configure(EntityTypeBuilder<TOne> builder)
    {
        builder.HasMany(_hasManyExpression!)
            .WithOne(_hasOneExpression!)
            .HasForeignKey(_foreignKeyExpression!);
    }
}