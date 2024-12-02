using Common.Domain;

namespace Repository.Repositories.Implementations;

public class IndexColumnRepository : Repository<IndexColumn>
{
    public IndexColumnRepository(DbDesignerContext context) : base(context)
    {
    }
}