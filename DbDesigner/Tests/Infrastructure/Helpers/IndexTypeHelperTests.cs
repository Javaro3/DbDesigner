using Common.Domain;
using Common.Dtos.IndexType;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class IndexTypeHelperTests
{
    private IndexTypeHelper _helper;
    private IQueryable<IndexType> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new IndexTypeHelper();
        _testData = new List<IndexType>
        {
            new()
            {
                Id = 1,
                Name = "Type1",
                Description = "First Type",
                DataBases = new List<DataBase> { new() { Id = 1 }, new() { Id = 2 } }
            },
            new()
            {
                Id = 2,
                Name = "Type2",
                Description = "Second Type",
                DataBases = new List<DataBase> { new() { Id = 2 }, new() { Id = 3 } }
            },
            new()
            {
                Id = 3,
                Name = "Type3",
                Description = "Third Type",
                DataBases = new List<DataBase> { new() { Id = 1 } }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersCorrectly()
    {
        var filter = new IndexTypeFilterDto { Description = "First" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Type1"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersCorrectly()
    {
        var filter = new IndexTypeFilterDto { Name = "Type2" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Type2"));
        });
    }

    [Test]
    public void ApplyFilter_WhenDataBasesMatch_FiltersCorrectly()
    {
        var filter = new IndexTypeFilterDto { DataBases = new List<int> { 1 } };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(x => x.Name == "Type1"), Is.True);
            Assert.That(result.Any(x => x.Name == "Type3"), Is.True);
        });
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersAreApplied_FiltersCorrectly()
    {
        var filter = new IndexTypeFilterDto
        {
            Description = "Type",
            DataBases = new List<int> { 2 }
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(x => x.Name == "Type1"), Is.True);
            Assert.That(result.Any(x => x.Name == "Type2"), Is.True);
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new IndexTypeFilterDto
        {
            Description = "Nonexistent",
            Name = "Nonexistent",
            DataBases = new List<int> { 999 }
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }
}