using Common.Domain;
using Common.Dtos.Orm;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class OrmHelperTests
{
    private OrmHelper _helper;
    private IQueryable<Orm> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new OrmHelper();
        _testData = new List<Orm>
        {
            new()
            {
                Id = 1,
                Name = "EntityFramework",
                Description = "ORM for .NET",
                Languages = new List<Language>
                {
                    new() { Id = 1, Name = "C#" },
                    new() { Id = 2, Name = "VB.NET" }
                }
            },
            new()
            {
                Id = 2,
                Name = "Dapper",
                Description = "Micro ORM for .NET",
                Languages = new List<Language>
                {
                    new() { Id = 1, Name = "C#" }
                }
            },
            new()
            {
                Id = 3,
                Name = "Hibernate",
                Description = "ORM for Java",
                Languages = new List<Language>
                {
                    new() { Id = 3, Name = "Java" }
                }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersCorrectly()
    {
        var filter = new OrmFilterDto { Description = "ORM for .NET" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("EntityFramework"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersCorrectly()
    {
        var filter = new OrmFilterDto { Name = "Dapper" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Dapper"));
        });
    }

    [Test]
    public void ApplyFilter_WhenLanguageMatches_FiltersCorrectly()
    {
        var filter = new OrmFilterDto { Languages = [1] };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersAreApplied_FiltersCorrectly()
    {
        var filter = new OrmFilterDto
        {
            Description = "ORM for .NET",
            Languages = [1]
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("EntityFramework"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new OrmFilterDto
        {
            Description = "Nonexistent ORM",
            Name = "Unknown ORM",
            Languages = [999]
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }
}