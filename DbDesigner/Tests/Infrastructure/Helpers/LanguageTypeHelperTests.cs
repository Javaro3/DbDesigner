using Common.Domain;
using Common.Dtos.LanguageType;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class LanguageTypeHelperTests
{
    private LanguageTypeHelper _helper;
    private IQueryable<LanguageType> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new LanguageTypeHelper();
        _testData = new List<LanguageType>
        {
            new()
            {
                Id = 1,
                Name = "Dynamic",
                Description = "Used for scripting",
                Language = new Language { Id = 1, Name = "Python" },
                SqlTypes = new List<SqlType>
                {
                    new() { Id = 1, Name = "VARCHAR" },
                    new() { Id = 2, Name = "TEXT" }
                }
            },
            new()
            {
                Id = 2,
                Name = "Static",
                Description = "Compiled language",
                Language = new Language { Id = 2, Name = "C#" },
                SqlTypes = new List<SqlType>
                {
                    new() { Id = 3, Name = "INT" }
                }
            },
            new()
            {
                Id = 3,
                Name = "Functional",
                Description = "Supports functional programming",
                Language = new Language { Id = 3, Name = "Haskell" },
                SqlTypes = new List<SqlType>
                {
                    new() { Id = 4, Name = "BOOLEAN" },
                    new() { Id = 5, Name = "CHAR" }
                }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersCorrectly()
    {
        var filter = new LanguageTypeFilterDto { Description = "scripting" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Dynamic"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersCorrectly()
    {
        var filter = new LanguageTypeFilterDto { Name = "Static" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Static"));
        });
    }

    [Test]
    public void ApplyFilter_WhenLanguageMatches_FiltersCorrectly()
    {
        var filter = new LanguageTypeFilterDto { Languages = new List<int> { 1 } };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Dynamic"));
        });
    }

    [Test]
    public void ApplyFilter_WhenSqlTypesMatch_FiltersCorrectly()
    {
        var filter = new LanguageTypeFilterDto { SqlTypes = new List<int> { 4 } };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Functional"));
        });
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersAreApplied_FiltersCorrectly()
    {
        var filter = new LanguageTypeFilterDto
        {
            Description = "Supports",
            SqlTypes = [4]
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Functional"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new LanguageTypeFilterDto
        {
            Description = "Nonexistent",
            Name = "Unknown",
            Languages = [999],
            SqlTypes = [999]
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }
}