using Common.GenerateModels;
using Infrastructure.Generator.ArchitectureBuilders;

namespace Tests.Infrastructure.Generator;

[TestFixture]
public class RepositoryBuilderTests
{
    private RepositoryBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _builder = new RepositoryBuilder();
    }

    [Test]
    public void AddName_ShouldSetArchitectureNameToRepository()
    {
        _builder.AddName();
        var result = _builder.Generate();
        Assert.That(result.Name, Is.EqualTo("Repository"));
    }

    [Test]
    public void AddInterfaces_ShouldAddIRepositoryInterface()
    {
        _builder.AddInterfaces();
        var result = _builder.Generate();

        Assert.That(result.InterfaceFiles, Has.Count.EqualTo(1));
        var (name, content) = result.InterfaceFiles[0];
        Assert.That(name, Is.EqualTo("IRepository"));
        StringAssert.Contains("public interface IRepository<T>", content);
        StringAssert.Contains("Task<T?> GetByIdAsync(int id);", content);
    }

    [Test]
    public void AddImplementation_ShouldAddRepositoryBaseClass()
    {
        var ormModel = new OrmGenerateModel
        {
            OrmName = "TestDbContext",
            Domains = new List<string>()
        };

        _builder.AddImplementation(ormModel);
        var result = _builder.Generate();

        Assert.That(result.ImplementationFiles, Has.Count.EqualTo(1));
        var (name, content) = result.ImplementationFiles[0];
        Assert.That(name, Is.EqualTo("Repository"));
        StringAssert.Contains("public class Repository<T>", content);
        StringAssert.Contains("protected readonly TestDbContext _context;", content);
    }

    [Test]
    public void Generate_ShouldReturnCompleteModel()
    {
        var ormModel = new OrmGenerateModel
        {
            OrmName = "TestDbContext",
            Domains = ["User", "Product"]
        };

        _builder.AddName()
                .AddInterfaces()
                .AddImplementation(ormModel);
        var result = _builder.Generate();

        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("Repository"));
            Assert.That(result.InterfaceFiles, Has.Count.EqualTo(1));
            Assert.That(result.ImplementationFiles, Has.Count.EqualTo(3));
        });
    }
}