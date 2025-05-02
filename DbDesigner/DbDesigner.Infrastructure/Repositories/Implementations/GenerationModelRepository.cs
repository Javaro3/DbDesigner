using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class GenerationModelRepository(DbDesignerContext context)
    : Repository<GenerationModel>(context);