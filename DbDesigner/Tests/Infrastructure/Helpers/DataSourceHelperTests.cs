using AutoMapper;
using Common.Dtos;
using Common.Enums;
using Infrastructure.Helpers;
using Moq;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class DataSourceHelperTests
{
    private DataSourceHelper _helper;
    private Mock<IMapper> _mapperMock;
    private IQueryable<TestEntity> _testData;

    [SetUp]
    public void SetUp()
    {
        _helper = new DataSourceHelper();
        _mapperMock = new Mock<IMapper>();

        _testData = new List<TestEntity>
        {
            new() { Id = 1, Name = "A" },
            new() { Id = 2, Name = "B" },
            new() { Id = 3, Name = "C" },
            new() { Id = 4, Name = "D" },
            new() { Id = 5, Name = "E" }
        }.AsQueryable();
    }

    [Test]
    public Task ApplyDataSource_ShouldThrowArgumentException_WhenSortFieldDoesNotExist()
    {
        var filter = new FilterRequestDto
        {
            PageNumber = 1,
            PageSize = 2,
            SortField = new SortFieldDto
            {
                Field = "NonExistentField",
                Direction = SortDirection.Asc
            }
        };

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _helper.ApplyDataSource<TestEntity, TestDto>(_testData, filter, _mapperMock.Object));
        return Task.CompletedTask;
    }
}

#region Helper Classes

public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

#endregion