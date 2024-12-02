using Common.Domain;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class RoleRepository : Repository<Role>
{
    public RoleRepository(DbDesignerContext context) : base(context)
    {
    }
}