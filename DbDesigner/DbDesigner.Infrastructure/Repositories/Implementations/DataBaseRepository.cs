using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class DataBaseRepository(DbDesignerContext context)
    : Repository<DataBase>(context);