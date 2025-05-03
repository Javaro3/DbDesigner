using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class ProjectRepository(DbDesignerContext context)
    : Repository<Project>(context)
{
    public override IQueryable<Project> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<Project?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Project> AddAsync(Project entity)
    {
        entity.DataBase = null;
        entity.User = null;
        await base.AddAsync(entity);
        return entity;
    }

    public override async Task<Project> UpdateAsync(Project entity)
    {
        entity.User = null;
        entity.DataBase = null;
        await base.UpdateAsync(entity);
        return entity;
    }
    
    private static IIncludableQueryable<Project, SqlType?> IncludeExpression(IQueryable<Project> query)
    {
        return query
            .Include(e => e.DataBase)
            .Include(e => e.User)
            .Include(e => e.Tables)
                .ThenInclude(e => e.Columns)
                    .ThenInclude(e => e.ColumnProperties)
                        .ThenInclude(e => e.Property)
            .Include(e => e.Tables)
                .ThenInclude(e => e.Columns)
                    .ThenInclude(e => e.SqlType);
    }
}