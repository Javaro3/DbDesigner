using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class LanguageRepository : Repository<Language>
{
    private readonly Expression<Func<Language, Language>> _selector = e => new Language
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Image = e.Image,
        Orms = e.Orms.Select(db => new Orm
        {
            Id = db.Id,
            Name = db.Name,
            Description = db.Description
        }).ToList()
    };
    
    public LanguageRepository(DbDesignerContext context) : base(context)
    {
    }
    
    public override IQueryable<Language> GetAll()
    {
        return _dbSet.Include(e => e.Orms)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<Language?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.Orms)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public override async Task UpdateAsync(Language entity)
    {
        var languageOrms = _context.LanguageOrms.Where(e => e.LanguageId == entity.Id).ToList();
        _context.RemoveRange(languageOrms);
        
        var orms = entity.Orms.ToList();
        entity.Orms.Clear();
        _dbSet.Update(entity);
        
        foreach (var orm in orms)
        {
            var languageOrm = new LanguageOrm { LanguageId = entity.Id, OrmId = orm.Id };
            await _context.AddAsync(languageOrm);
        }
        await _context.SaveChangesAsync();
    }
}