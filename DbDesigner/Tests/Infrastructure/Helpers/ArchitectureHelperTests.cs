using Common.Domain;
using Common.Dtos.Architecture;
using Infrastructure.Helpers;

namespace Tests.Infrastructure.Helpers;

[TestFixture]
public class ArchitectureHelperTests
{
    private ArchitectureHelper _helper;
    private IQueryable<Architecture> _architectures;

    [SetUp]
    public void SetUp()
    {
        _helper = new ArchitectureHelper();

        _architectures = new List<Architecture>
        {
            new() { Id = 1, Name = "Clean Architecture", Description = "An example of clean code architecture" },
            new() { Id = 2, Name = "Microservices", Description = "An architecture style for distributed systems" },
            new() { Id = 3, Name = "Monolithic", Description = "A traditional architecture for building applications" },
            new() { Id = 4, Name = "Serverless", Description = "An event-driven architecture using cloud services" },
        }.AsQueryable();
    }

    [Test]
    public void ApplyFilter_ShouldFilterByName()
    {
        var filter = new ArchitectureFilterDto
        {
            Name = "micro"
        };

        var result = _helper.ApplyFilter(_architectures, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Microservices"));
    }

    [Test]
    public void ApplyFilter_ShouldFilterByDescription()
    {
        var filter = new ArchitectureFilterDto
        {
            Description = "traditional"
        };

        var result = _helper.ApplyFilter(_architectures, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Monolithic"));
    }

    [Test]
    public void ApplyFilter_ShouldFilterByNameAndDescription()
    {
        var filter = new ArchitectureFilterDto
        {
            Name = "serverless",
            Description = "cloud"
        };

        var result = _helper.ApplyFilter(_architectures, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Serverless"));
    }

    [Test]
    public void ApplyFilter_ShouldReturnAll_WhenNoFilterIsApplied()
    {
        var filter = new ArchitectureFilterDto();

        var result = _helper.ApplyFilter(_architectures, filter).ToList();

        Assert.That(result, Has.Count.EqualTo(4));
    }

    [Test]
    public void ApplySort_ShouldReturnQueryUnchanged()
    {
        var filter = new ArchitectureFilterDto();

        var result = _helper.ApplySort(_architectures, filter).ToList();

        CollectionAssert.AreEqual(_architectures.ToList(), result);
    }
}