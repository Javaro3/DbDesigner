using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class SqlTypeRepository : Repository<SqlType>
{
    private readonly Expression<Func<SqlType, SqlType>> _selector = x => new SqlType
    {
        Id = x.Id,
        Name = x.Name,
        Description = x.Description,
        HasParams = x.HasParams,
        DataBases = x.DataBases.Select(e => new DataBase
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description
        }).ToList(),
        LanguageTypes = x.LanguageTypes.Select(e => new LanguageType
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description
        }).ToList()
    };
    
    public SqlTypeRepository(DbDesignerContext context) : base(context)
    {
    }

    public override IQueryable<SqlType> GetAll()
    {
        return _dbSet.Include(e => e.DataBases)
            .Include(e => e.LanguageTypes)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<SqlType?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.DataBases)
            .Include(e => e.LanguageTypes)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task UpdateAsync(SqlType entity)
    {
        var databaseSqlTypes = _context.DataBaseTypes.Where(e => e.SqlTypeId == entity.Id).ToList();
        _context.RemoveRange(databaseSqlTypes);
        
        var dataBases = entity.DataBases.ToList();
        entity.DataBases.Clear();
        _dbSet.Update(entity);
        
        foreach (var dataBase in dataBases)
        {
            var dataBaseSqlType = new DataBaseType { SqlTypeId = entity.Id, DataBaseId = dataBase.Id };
            await _context.AddAsync(dataBaseSqlType);
        }
        await _context.SaveChangesAsync();
    }

    public override async Task AddAsync(SqlType entity)
    {
        var dataBases = entity.DataBases.ToList();
        entity.DataBases.Clear();
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        
        foreach (var dataBase in dataBases)
        {
            var dataBaseSqlType = new DataBaseType { SqlTypeId = entity.Id, DataBaseId = dataBase.Id };
            await _context.AddAsync(dataBaseSqlType);
        }
        await _context.SaveChangesAsync();
    }
}