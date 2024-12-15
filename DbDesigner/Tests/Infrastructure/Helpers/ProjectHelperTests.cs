using Common.Domain;
using Common.Dtos.Project;
using Infrastructure.Helpers;
using static NUnit.Framework.Assert;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class ProjectHelperTests
{
    private ProjectHelper _helper;
    private IQueryable<Project> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new ProjectHelper();
        _testData = new List<Project>
        {
            new()
            {
                Id = 1,
                Name = "Project A",
                Description = "First Project",
                CreatedOn = DateTime.UtcNow.AddMonths(-1),
                Users = new List<User>
                {
                    new() { Id = 1, Name = "User 1" }
                },
                DataBaseId = 1
            },
            new()
            {
                Id = 2,
                Name = "Project B",
                Description = "Second Project",
                CreatedOn = DateTime.UtcNow.AddMonths(-2),
                Users = new List<User>
                {
                    new() { Id = 2, Name = "User 2" }
                },
                DataBaseId = 2
            },
            new()
            {
                Id = 3,
                Name = "Project C",
                Description = "Third Project",
                CreatedOn = DateTime.UtcNow.AddMonths(-3),
                Users = new List<User>
                {
                    new() { Id = 3, Name = "User 3" }
                },
                DataBaseId = 1
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_WhenCurrentUserIsSet_FiltersByCurrentUser()
    {
        var filter = new ProjectFilterDto { CurrentUser = 1 };

        var result = _helper.ApplyFilter(_testData, filter);

        Multiple(() =>
        {
            That(result.Count(), Is.EqualTo(1));
            That(result.First().Name, Is.EqualTo("Project A"));
        });
    }

    [Test]
    public void ApplyFilter_WhenNameMatches_FiltersByName()
    {
        var filter = new ProjectFilterDto { Name = "Project B" };

        var result = _helper.ApplyFilter(_testData, filter);

        Multiple(() =>
        {
            That(result.Count(), Is.EqualTo(1));
            That(result.First().Name, Is.EqualTo("Project B"));
        });
    }

    [Test]
    public void ApplyFilter_WhenDescriptionMatches_FiltersByDescription()
    {
        var filter = new ProjectFilterDto { Description = "Second Project" };

        var result = _helper.ApplyFilter(_testData, filter);

        Multiple(() =>
        {
            That(result.Count(), Is.EqualTo(1));
            That(result.First().Name, Is.EqualTo("Project B"));
        });
    }

    [Test]
    public void ApplyFilter_WhenCreatedOnFromIsSet_FiltersByCreatedOnFrom()
    {
        var filter = new ProjectFilterDto { CreatedOnFrom = DateTime.UtcNow.AddMonths(-2) };

        var result = _helper.ApplyFilter(_testData, filter);

        That(result.Count(), Is.EqualTo(1)); 
    }

    [Test]
    public void ApplyFilter_WhenCreatedOnToIsSet_FiltersByCreatedOnTo()
    {
        var filter = new ProjectFilterDto { CreatedOnTo = DateTime.UtcNow.AddMonths(-1) };

        var result = _helper.ApplyFilter(_testData, filter);

        That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public void ApplyFilter_WhenUsersMatches_FiltersByUsers()
    {
        var filter = new ProjectFilterDto { Users = [1] };

        var result = _helper.ApplyFilter(_testData, filter);

        Multiple(() =>
        {
            That(result.Count(), Is.EqualTo(1));
            That(result.First().Name, Is.EqualTo("Project A"));
        });
    }

    [Test]
    public void ApplyFilter_WhenDataBasesMatches_FiltersByDataBase()
    {
        var filter = new ProjectFilterDto { DataBases = [1] };

        var result = _helper.ApplyFilter(_testData, filter);

        That(result.Count(), Is.EqualTo(2)); 
    }

    [Test]
    public void ApplyFilter_WhenNoMatch_ReturnsEmpty()
    {
        var filter = new ProjectFilterDto
        {
            CurrentUser = 999,
            Name = "Nonexistent Project",
            Description = "Nonexistent Description",
            CreatedOnFrom = DateTime.UtcNow.AddYears(1),
            CreatedOnTo = DateTime.UtcNow.AddYears(1),
            Users = [999],
            DataBases = [999]
        };

        var result = _helper.ApplyFilter(_testData, filter);
        That(result, Is.Empty);
    }
}