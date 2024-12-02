using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class LanguageTypeRepository : Repository<LanguageType>
{
    private readonly Expression<Func<LanguageType, LanguageType>> _selector = e => new LanguageType
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        LanguageId = e.LanguageId,
        Language = e.Language != null 
            ? new Language 
            {
                Id = e.Language.Id,
                Name = e.Language.Name,
                Description = e.Description
            }
            : null,
        SqlTypes = e.SqlTypes.Select(x => new SqlType
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            HasParams = x.HasParams
        }).ToList()
    };
    
    public LanguageTypeRepository(DbDesignerContext context) : base(context)
    {
    }
    
    public override IQueryable<LanguageType> GetAll()
    {
        return _dbSet.Include(e => e.SqlTypes)
            .Include(e => e.Language)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<LanguageType?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.SqlTypes)
            .Include(e => e.Language)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public override async Task UpdateAsync(LanguageType entity)
    {
        entity.LanguageId = entity.Language!.Id;
        entity.Language = null;
        
        var languageTypeSqlTypes = _context.LanguageTypeSqlTypes.Where(e => e.LanguageTypeId == entity.Id).ToList();
        _context.RemoveRange(languageTypeSqlTypes);
        
        var sqlTypes = entity.SqlTypes.ToList();
        entity.SqlTypes.Clear();
        _dbSet.Update(entity);
        
        foreach (var sqlty in sqlTypes)
        {
            var languageTypeSqlType = new LanguageTypeSqlType { LanguageTypeId = entity.Id, SqlTypeId = sqlty.Id };
            await _context.AddAsync(languageTypeSqlType);
        }
        
        await _context.SaveChangesAsync();
    }

    public override async Task AddAsync(LanguageType entity)
    {
        entity.LanguageId = entity.Language!.Id;
        entity.Language = null;
        
        var sqlTypes = entity.SqlTypes.ToList();
        entity.SqlTypes.Clear();
        _dbSet.Update(entity);
        
        foreach (var sqlty in sqlTypes)
        {
            var languageTypeSqlType = new LanguageTypeSqlType { LanguageTypeId = entity.Id, SqlTypeId = sqlty.Id };
            await _context.AddAsync(languageTypeSqlType);
        }
        
        await _context.SaveChangesAsync();

    }
}