using Common.Domain;
using Common.Dtos.Role;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class RoleHelperTests
{
    private RoleHelper _helper;
    private IQueryable<Role> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new RoleHelper();
        _testData = new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "Admin",
                Description = "Administrator role"
            },
            new()
            {
                Id = 2,
                Name = "User",
                Description = "Regular user role"
            },
            new()
            {
                Id = 3,
                Name = "Manager",
                Description = "Manager role"
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersByDescription()
    {
        var filter = new RoleFilterDto { Description = "Administrator role" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Admin"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new RoleFilterDto { Name = "Manager" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Manager"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new RoleFilterDto
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
        var filter = new RoleFilterDto
        {
            Description = "role",
            Name = "Admin"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(1)); 
    }

    [Test]
    public void ApplyFilter_WhenDescriptionAndNameMatch_ReturnsCorrectResults()
    {
        var filter = new RoleFilterDto
        {
            Description = "Manager role",
            Name = "Manager"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Manager"));
        });
    }
}