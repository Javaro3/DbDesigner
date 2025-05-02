using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class IndexColumnRepository(DbDesignerContext context)
    : Repository<IndexColumn>(context);