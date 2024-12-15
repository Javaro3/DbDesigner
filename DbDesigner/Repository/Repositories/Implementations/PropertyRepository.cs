using Common.Domain;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class PropertyRepository : Repository<Property>
{
    public PropertyRepository(DbDesignerContext context) : base(context)
    {
    }
}