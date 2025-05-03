using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class ColumnPropertyRepository(DbDesignerContext context)
    : Repository<ColumnProperty>(context);