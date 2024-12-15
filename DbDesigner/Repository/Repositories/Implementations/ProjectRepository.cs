using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    private readonly Expression<Func<Project, Project>> _cardSelector = p => new Project
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        CreatedOn = p.CreatedOn,
        DataBaseId = p.DataBaseId,
        DataBase = p.DataBase != null
            ? new DataBase 
            { 
                Id = p.DataBase.Id,
                Name = p.DataBase.Name,
                Description = p.DataBase.Description,
                
            }
            : null,
        Users = p.Users.Select(e => new User
        {
            Id = e.Id,
            Name = e.Name,
            Email = e.Email,
            CreatedOn = e.CreatedOn
        }).ToList()
    };
    
    private readonly Expression<Func<Project, Project>> _diagramSelector = project => new Project
    {
        Id = project.Id,
        Name = project.Name,
        DataBaseId = project.DataBaseId,
        DataBase = project.DataBase != null
            ? new DataBase 
            { 
                Id = project.DataBase.Id,
                Name = project.DataBase.Name,
                Description = project.DataBase.Description,
                
            }
            : null,
        Tables = project.Tables.Select(table => new Table
        {
            Id = table.Id,
            Name = table.Name,
            Description = table.Description,
            Columns = table.Columns.Select(column => new Column
            {
                Id = column.Id,
                Name = column.Name,
                Description = column.Description,
                SqlTypeId = column.SqlTypeId,
                SqlTypeParams = column.SqlTypeParams
            }).ToList(),
        }).ToList()
    };

    private readonly ITableRepository _tableRepository;
    
    public ProjectRepository(DbDesignerContext context, ITableRepository tableRepository) : base(context)
    {
        _tableRepository = tableRepository;
    }
    
    public override IQueryable<Project> GetAll()
    {
        return _dbSet.Include(e => e.DataBase)
            .Include(e => e.Users)
            .Select(_cardSelector)
            .AsQueryable();
    }
    
    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.DataBase)
            .Include(e => e.Users)
            .Select(_cardSelector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task AddAsync(Project entity)
    {
        entity.DataBaseId = entity.DataBase!.Id;
        entity.DataBase = null;
        
        var users = entity.Users.ToList();
        entity.Users.Clear();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        foreach (var user in users)
        {
            var userProject = new UserProject { ProjectId = entity.Id, UserId = user.Id };
            await _context.AddAsync(userProject);
        }
        
        await _context.SaveChangesAsync();
    }

    public override async Task UpdateAsync(Project entity)
    {
        entity.DataBaseId = entity.DataBase!.Id;
        entity.DataBase = null;
        
        var userProjects = _context.UserProjects.Where(e => e.ProjectId == entity.Id).ToList();
        _context.RemoveRange(userProjects);
        
        var users = entity.Users.ToList();
        entity.Users.Clear();
        _dbSet.Update(entity);
        
        foreach (var user in users)
        {
            var userProject = new UserProject { ProjectId = entity.Id, UserId = user.Id };
            await _context.AddAsync(userProject);
        }
        
        await _context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(Project entity)
    {
        var tableIds = await _context.ProjectTables.Where(e => e.ProjectId == entity.Id).Select(e => e.TableId).ToListAsync();
        var tables = await _context.Tables.Where(e => tableIds.Contains(e.Id)).ToListAsync();

        foreach (var table in tables)
        {
            await _tableRepository.DeleteAsync(table);
        }

        await base.DeleteAsync(entity);
    }

    public async Task<Project?> GetForDiagramByIdAsync(int id)
    {
        return await _dbSet
            .Include(e => e.DataBase)
            .Include(e => e.Tables)
                .ThenInclude(e => e.Columns)
                    .ThenInclude(e => e.SqlType)
            .Select(_diagramSelector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}