using Common.Domain;
using Common.Dtos.RelationAction;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class RelationActionHelperTests
{
    private RelationActionHelper _helper;
    private IQueryable<RelationAction> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new RelationActionHelper();
        _testData = new List<RelationAction>
        {
            new()
            {
                Id = 1,
                Name = "Action A",
                Description = "First Action"
            },
            new()
            {
                Id = 2,
                Name = "Action B",
                Description = "Second Action"
            },
            new()
            {
                Id = 3,
                Name = "Action C",
                Description = "Third Action"
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersByDescription()
    {
        var filter = new RelationActionFilterDto { Description = "Second Action" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Action B"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new RelationActionFilterDto { Name = "Action A" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Action A"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new RelationActionFilterDto
        {
            Description = "Nonexistent Description",
            Name = "Nonexistent Name"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersMatch_ReturnsCorrectResults()
    {
        var filter = new RelationActionFilterDto
        {
            Description = "Action",
            Name = "Action A"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public void ApplyFilter_WhenDescriptionAndNameMatch_ReturnsCorrectResults()
    {
        var filter = new RelationActionFilterDto
        {
            Description = "Third Action",
            Name = "Action C"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Action C"));
        });
    }
}