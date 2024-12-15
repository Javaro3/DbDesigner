using Common.Domain;
using Common.Dtos.SqlType;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class SqlTypeHelperTests
{
    private SqlTypeHelper _helper;
    private IQueryable<SqlType> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new SqlTypeHelper();
        _testData = new List<SqlType>
        {
            new()
            {
                Id = 1,
                Name = "Integer",
                Description = "SQL Integer type",
                HasParams = true,
                DataBases = new List<DataBase> { new() { Id = 1 }, new() { Id = 2 } },
                LanguageTypes = new List<LanguageType> { new() { Id = 1 } }
            },
            new()
            {
                Id = 2,
                Name = "Varchar",
                Description = "SQL Varchar type",
                HasParams = false,
                DataBases = new List<DataBase> { new() { Id = 2 } },
                LanguageTypes = new List<LanguageType> { new() { Id = 2 } }
            },
            new()
            {
                Id = 3,
                Name = "Decimal",
                Description = "SQL Decimal type",
                HasParams = true,
                DataBases = new List<DataBase> { new() { Id = 3 } },
                LanguageTypes = new List<LanguageType> { new() { Id = 1 }, new() { Id = 2 } }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersByDescription()
    {
        var filter = new SqlTypeFilterDto { Description = "SQL Integer type" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Integer"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new SqlTypeFilterDto { Name = "Varchar" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Varchar"));
        });
    }

    [Test]
    public void ApplyFilter_WhenHasParamsMatches_FiltersByHasParams()
    {
        var filter = new SqlTypeFilterDto { HasParams = true };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new SqlTypeFilterDto
        {
            Description = "Nonexistent type",
            Name = "Nonexistent name"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ApplyFilter_WhenDataBasesMatch_FiltersByDataBases()
    {
        var filter = new SqlTypeFilterDto { DataBases = [2] };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenLanguageTypesMatch_FiltersByLanguageTypes()
    {
        var filter = new SqlTypeFilterDto { LanguageTypes = [1] };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersMatch_ReturnsCorrectResults()
    {
        var filter = new SqlTypeFilterDto
        {
            Description = "SQL type",
            HasParams = true
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(0));
    }
}