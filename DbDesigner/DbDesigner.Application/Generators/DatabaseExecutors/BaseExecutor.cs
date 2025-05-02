using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators.DatabaseExecutors;

public abstract class BaseExecutor
{
    protected readonly DatabaseConfigOptions? Options;

    protected BaseExecutor(DatabaseConfigOptions? options)
    {
        Options = options;
    }
}