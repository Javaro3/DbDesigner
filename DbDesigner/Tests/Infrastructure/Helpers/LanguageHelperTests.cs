using Common.Domain;
using Common.Dtos.Language;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class LanguageHelperTests
{
    private LanguageHelper _helper;
    private IQueryable<Language> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new LanguageHelper();
        _testData = new List<Language>
        {
            new()
            {
                Id = 1,
                Name = "C#",
                Description = "Modern programming language",
                Orms = new List<Orm>
                {
                    new() { Id = 1, Name = "Entity Framework" },
                    new() { Id = 2, Name = "Dapper" }
                }
            },
            new()
            {
                Id = 2,
                Name = "Java",
                Description = "Widely used programming language",
                Orms = new List<Orm>
                {
                    new() { Id = 3, Name = "Hibernate" }
                }
            },
            new()
            {
                Id = 3,
                Name = "Python",
                Description = "Popular programming language for data science",
                Orms = new List<Orm>
                {
                    new() { Id = 4, Name = "SQLAlchemy" },
                    new() { Id = 5, Name = "Django ORM" }
                }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersCorrectly()
    {
        var filter = new LanguageFilterDto { Description = "Modern" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("C#"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersCorrectly()
    {
        var filter = new LanguageFilterDto { Name = "Java" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Java"));
        });
    }

    [Test]
    public void ApplyFilter_WhenOrmsMatch_FiltersCorrectly()
    {
        var filter = new LanguageFilterDto { Orms = new List<int> { 2 } };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("C#"));
        });
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersAreApplied_FiltersCorrectly()
    {
        var filter = new LanguageFilterDto
        {
            Description = "programming",
            Orms = new List<int> { 4 }
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Python"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new LanguageFilterDto
        {
            Description = "Nonexistent",
            Name = "Nonexistent",
            Orms = new List<int> { 999 }
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }
}