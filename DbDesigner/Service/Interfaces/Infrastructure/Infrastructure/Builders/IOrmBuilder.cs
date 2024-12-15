using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IOrmBuilder : IBuilder<string>
{
    public IOrmBuilder AddOrm(OrmGenerateModel model);
}