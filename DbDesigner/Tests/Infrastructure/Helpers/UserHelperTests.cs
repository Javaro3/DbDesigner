using Common.Domain;
using Common.Dtos.User;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class UserHelperTests
{
    private UserHelper _helper;
    private IQueryable<User> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new UserHelper();
        _testData = new List<User>
        {
            new()
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com",
                CreatedOn = DateTime.UtcNow.AddDays(-1),
                Roles = new List<Role> { new() { Id = 1 }, new() { Id = 2 } }
            },
            new()
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane@example.com",
                CreatedOn = DateTime.UtcNow.AddDays(-10),
                Roles = new List<Role> { new() { Id = 2 } }
            },
            new()
            {
                Id = 3,
                Name = "Bob Johnson",
                Email = "bob@example.com",
                CreatedOn = DateTime.UtcNow.AddDays(-5),
                Roles = new List<Role> { new() { Id = 1 } }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenEmailMatches_FiltersByEmail()
    {
        var filter = new UserFilterDto { Email = "john@example.com" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Email, Is.EqualTo("john@example.com"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new UserFilterDto { Name = "Jane Smith" };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Jane Smith"));
        });
    }

    [Test]
    public void ApplyFilter_WhenCreatedOnFromMatches_FiltersByCreatedOnFrom()
    {
        var filter = new UserFilterDto { CreatedOnFrom = DateTime.UtcNow.AddDays(-7) };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenCreatedOnToMatches_FiltersByCreatedOnTo()
    {
        var filter = new UserFilterDto { CreatedOnTo = DateTime.UtcNow.AddDays(-3) };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenRolesMatch_FiltersByRoles()
    {
        var filter = new UserFilterDto { Roles = [1] };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new UserFilterDto
        {
            Email = "nonexistent@example.com",
            Name = "Nonexistent User"
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ApplyFilter_WhenMultipleFiltersMatch_ReturnsCorrectResults()
    {
        var filter = new UserFilterDto
        {
            Email = "john@example.com",
            Roles = [1]
        };

        var result = _helper.ApplyFilter(_testData, filter);

        Assert.That(result.Count(), Is.EqualTo(1)); 
    }
}