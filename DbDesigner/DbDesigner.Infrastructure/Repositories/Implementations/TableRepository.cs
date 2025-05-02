using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class TableRepository(DbDesignerContext context)
    : Repository<Table>(context);