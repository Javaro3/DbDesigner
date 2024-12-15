using Common.Attributes;
using Common.Enums;
using Common.Extensions;

namespace Tests.Common;

public enum SampleEnum
{
    [Name("Sample Name")]
    [Image("sample-image.png")]
    [System.ComponentModel.Description("Sample description")]
    [Language(LanguageEnum.Python, LanguageEnum.CSharp)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql)]
    [SqlType(SqlTypeEnum.Int, SqlTypeEnum.Bit)]
    [HasParams(true)]
    Value1,

    Value2
}

[TestFixture]
public class EnumExtensionTests
{
    [Test]
    public void GetName_ShouldReturnCustomName()
    {
        var name = SampleEnum.Value1.GetName();
        Assert.That(name, Is.EqualTo("Sample Name"));
    }

    [Test]
    public void GetName_ShouldReturnDefaultName_WhenNoAttribute()
    {
        var name = SampleEnum.Value2.GetName();
        Assert.That(name, Is.EqualTo("Value2"));
    }

    [Test]
    public void GetImage_ShouldReturnCustomImage()
    {
        var image = SampleEnum.Value1.GetImage();
        Assert.That(image, Is.EqualTo("sample-image.png"));
    }

    [Test]
    public void GetImage_ShouldReturnDefaultName_WhenNoAttribute()
    {
        var image = SampleEnum.Value2.GetImage();
        Assert.That(image, Is.EqualTo("Value2"));
    }

    [Test]
    public void GetDescription_ShouldReturnCustomDescription()
    {
        var description = SampleEnum.Value1.GetDescription();
        Assert.That(description, Is.EqualTo("Sample description"));
    }

    [Test]
    public void GetDescription_ShouldReturnDefaultName_WhenNoAttribute()
    {
        var description = SampleEnum.Value2.GetDescription();
        Assert.That(description, Is.EqualTo("Value2"));
    }

    [Test]
    public void GetLanguages_ShouldReturnCustomLanguages()
    {
        var languages = SampleEnum.Value1.GetLanguages();
        Assert.That(languages, Is.EqualTo(new[] { LanguageEnum.Python, LanguageEnum.CSharp }));
    }

    [Test]
    public void GetLanguages_ShouldReturnEmptyList_WhenNoAttribute()
    {
        var languages = SampleEnum.Value2.GetLanguages();
        Assert.That(languages, Is.Empty);
    }

    [Test]
    public void GetDataBases_ShouldReturnCustomDataBases()
    {
        var databases = SampleEnum.Value1.GetDataBases();
        Assert.That(databases, Is.EqualTo(new[] { DataBaseEnum.Mssql, DataBaseEnum.MySql }));
    }

    [Test]
    public void GetDataBases_ShouldReturnEmptyList_WhenNoAttribute()
    {
        var databases = SampleEnum.Value2.GetDataBases();
        Assert.That(databases, Is.Empty);
    }

    [Test]
    public void GetSqlTypes_ShouldReturnCustomSqlTypes()
    {
        var sqlTypes = SampleEnum.Value1.GetSqlTypes();
        Assert.That(sqlTypes, Is.EqualTo(new[] { SqlTypeEnum.Int, SqlTypeEnum.Bit }));
    }

    [Test]
    public void GetSqlTypes_ShouldReturnEmptyList_WhenNoAttribute()
    {
        var sqlTypes = SampleEnum.Value2.GetSqlTypes();
        Assert.That(sqlTypes, Is.Empty);
    }

    [Test]
    public void GetParams_ShouldReturnTrue_WhenHasParams()
    {
        var hasParams = SampleEnum.Value1.GetParams();
        Assert.That(hasParams, Is.True);
    }

    [Test]
    public void GetParams_ShouldReturnFalse_WhenNoAttribute() 
    {
        var hasParams = SampleEnum.Value2.GetParams();
        Assert.That(hasParams, Is.False);
    }
}