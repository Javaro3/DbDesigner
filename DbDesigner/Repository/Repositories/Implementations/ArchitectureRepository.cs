using Common.Domain;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class ArchitectureRepository : Repository<Architecture>
{
    public ArchitectureRepository(DbDesignerContext context) : base(context)
    {
    }
}