using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class ArchitectureRepository(DbDesignerContext context)
    : Repository<Architecture>(context);