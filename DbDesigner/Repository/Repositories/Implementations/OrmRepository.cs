using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class OrmRepository : Repository<Orm> 
{
    private readonly Expression<Func<Orm, Orm>> _selector = e => new Orm
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Languages = e.Languages.Select(db => new Language
        {
            Id = db.Id,
            Name = db.Name,
            Description = db.Description,
            Image = db.Image
        }).ToList()
    };
    
    public OrmRepository(DbDesignerContext context) : base(context)
    {
    }
    
    public override IQueryable<Orm> GetAll()
    {
        return _dbSet.Include(e => e.Languages)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<Orm?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.Languages)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public override async Task UpdateAsync(Orm entity)
    {
        var languageOrms = _context.LanguageOrms.Where(e => e.OrmId == entity.Id).ToList();
        _context.RemoveRange(languageOrms);
        
        var languages = entity.Languages.ToList();
        entity.Languages.Clear();
        _dbSet.Update(entity);
        
        foreach (var language in languages)
        {
            var languageOrm = new LanguageOrm { OrmId = entity.Id, LanguageId = language.Id };
            await _context.AddAsync(languageOrm);
        }
        await _context.SaveChangesAsync();
    }
}