using Common.Domain;
using Common.Dtos.DataBase;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class DataBaseHelperTests
{
    private DataBaseHelper _helper;
    private IQueryable<DataBase> _dataBases;
    private static readonly string[] expected = ["SQL Database", "In-Memory Database"];
    private static readonly string[] expectedArray = ["SQL Database", "In-Memory Database"];

    [SetUp]
    public void SetUp()
    {
        _helper = new DataBaseHelper();

        _dataBases = new List<DataBase>
        {
            new()
            {
                Id = 1, 
                Name = "SQL Database", 
                Description = "Relational database system", 
                IndexTypes = new List<IndexType> { new() { Id = 1 }, new() { Id = 2 } }
            },
            new()
            {
                Id = 2, 
                Name = "NoSQL Database", 
                Description = "Non-relational database system", 
                IndexTypes = new List<IndexType> { new() { Id = 3 } }
            },
            new()
            {
                Id = 3, 
                Name = "In-Memory Database", 
                Description = "Database optimized for in-memory storage", 
                IndexTypes = new List<IndexType> { new() { Id = 1 } }
            }
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_ShouldFilterByName()
    {
        var filter = new DataBaseFilterDto
        {
            Name = "sql"
        };

        var result = _helper.ApplyFilter(_dataBases, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("SQL Database"));
    }

    [Test]
    public void ApplyFilter_ShouldFilterByDescription()
    {
        var filter = new DataBaseFilterDto
        {
            Description = "non-relational"
        };

        var result = _helper.ApplyFilter(_dataBases, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("NoSQL Database"));
    }

    [Test]
    public void ApplyFilter_ShouldFilterByIndexTypes()
    {
        var filter = new DataBaseFilterDto
        {
            IndexTypes = new List<int> { 1 }
        };

        var result = _helper.ApplyFilter(_dataBases, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        CollectionAssert.AreEqual(expected, result.Select(x => x.Name));
    }

    [Test]
    public void ApplyFilter_ShouldFilterByMultipleCriteria()
    {
        var filter = new DataBaseFilterDto
        {
            Name = "Database",
            IndexTypes = new List<int> { 1 }
        };

        var result = _helper.ApplyFilter(_dataBases, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        CollectionAssert.AreEqual(expectedArray, result.Select(x => x.Name));
    }

    [Test]
    public void ApplyFilter_ShouldReturnAll_WhenNoFilterIsApplied()
    {
        var filter = new DataBaseFilterDto();

        var result = _helper.ApplyFilter(_dataBases, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(3));
    }

    [Test]
    public void ApplySort_ShouldReturnQueryUnchanged()
    {
        var filter = new DataBaseFilterDto();

        var result = _helper.ApplySort(_dataBases, filter).ToList();

        CollectionAssert.AreEqual(_dataBases.ToList(), result);
    }
}