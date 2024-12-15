using Common.Domain;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class RelationActionRepository : Repository<RelationAction>
{
    public RelationActionRepository(DbDesignerContext context) : base(context)
    {
    }
}