using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class RoleRepository(DbDesignerContext context)
    : Repository<Role>(context);
