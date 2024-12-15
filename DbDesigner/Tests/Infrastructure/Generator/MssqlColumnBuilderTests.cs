using Common.GenerateModels;
using Infrastructure.Generator.ColumnBuilders;

namespace Tests.Infrastructure.Generator;

[TestFixture]
public class MssqlColumnBuilderTests
{
    private MssqlColumnBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _builder = new MssqlColumnBuilder();
    }

    [Test]
    public void AddName_WhenCalled_AddsColumnNameToScript()
    {
        var columnName = "ColumnName";

        _builder.AddName(columnName);
        var result = _builder.Generate();

        Assert.That(result, Is.EqualTo("\tColumnName ,\n"));
    }

    [Test]
    public void AddSqlType_WhenCalled_AddsSqlTypeToScript()
    {
        var sqlType = new SqlTypeGenerateModel { SqlTypeName = "VARCHAR", Params = "255" };

        _builder.AddName("ColumnName").AddSqlType(sqlType);
        var result = _builder.Generate();

        Assert.That(result, Is.EqualTo("\tColumnName VARCHAR(255) ,\n"));
    }

    [Test]
    public void AddSqlType_WhenNoParamsProvided_OnlyAddsSqlTypeName()
    {
        var sqlType = new SqlTypeGenerateModel { SqlTypeName = "INT", Params = null };

        _builder.AddName("ColumnName").AddSqlType(sqlType);
        var result = _builder.Generate();

        Assert.That(result, Is.EqualTo("\tColumnName INT ,\n"));
    }
}