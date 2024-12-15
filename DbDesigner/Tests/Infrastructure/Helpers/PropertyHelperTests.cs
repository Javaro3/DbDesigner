using Common.Domain;
using Common.Dtos.Property;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class PropertyHelperTests
{
    private PropertyHelper _helper;
    private IQueryable<Property> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new PropertyHelper();
        _testData = new List<Property>
        {
            new()
            {
                Id = 1,
                Name = "Property A",
                Description = "First Property",
                HasParams = true
            },
            new()
            {
                Id = 2,
                Name = "Property B",
                Description = "Second Property",
                HasParams = false
            },
            new()
            {
                Id = 3,
                Name = "Property C",
                Description = "Third Property",
                HasParams = true
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersByDescription()
    {
        var filter = new PropertyFilterDto { Description = "Second Property" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property B"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new PropertyFilterDto { Name = "Property A" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property A"));
        });
    }

    [Test]
    public void ApplyFilter_WhenHasParamsIsTrue_FiltersByHasParams()
    {
        var filter = new PropertyFilterDto { HasParams = true };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ApplyFilter_WhenHasParamsIsFalse_FiltersByHasParamsFalse()
    {
        var filter = new PropertyFilterDto { HasParams = false };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new PropertyFilterDto
        {
            Description = "Nonexistent Description",
            Name = "Nonexistent Name",
            HasParams = true
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersMatch_ReturnsCorrectResults()
    {
        var filter = new PropertyFilterDto
        {
            Description = "Property",
            HasParams = true
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2));
    }
}