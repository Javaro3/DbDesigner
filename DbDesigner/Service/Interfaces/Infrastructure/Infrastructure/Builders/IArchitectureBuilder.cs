using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IArchitectureBuilder : IBuilder<ArchitectureGenerateModel>
{
    IArchitectureBuilder AddName();
    
    IArchitectureBuilder AddInterfaces();
    
    IArchitectureBuilder AddImplementation(OrmGenerateModel ormModel);
}