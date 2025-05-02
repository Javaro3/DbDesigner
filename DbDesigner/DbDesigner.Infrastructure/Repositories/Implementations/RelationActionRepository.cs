using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class RelationActionRepository(DbDesignerContext context)
    : Repository<RelationAction>(context);