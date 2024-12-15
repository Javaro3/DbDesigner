using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class DataBaseRepository : Repository<DataBase>
{
    private readonly Expression<Func<DataBase, DataBase>> _selector = e => new DataBase
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Image = e.Image,
        IndexTypes = e.IndexTypes.Select(db => new IndexType
        {
            Id = db.Id,
            Name = db.Name,
            Description = db.Description,
        }).ToList()
    };
    
    
    public DataBaseRepository(DbDesignerContext context) : base(context)
    {
    }
    
    public override IQueryable<DataBase> GetAll()
    {
        return _dbSet.Include(e => e.IndexTypes)
            .Select(_selector)
            .AsQueryable();
    }

    public override async Task<DataBase?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.IndexTypes)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task UpdateAsync(DataBase entity)
    {
        var databaseIndexTypes = _context.DataBaseIndexTypes.Where(e => e.DataBaseId == entity.Id).ToList();
        _context.RemoveRange(databaseIndexTypes);
        
        var indexTypes = entity.IndexTypes.ToList();
        entity.IndexTypes.Clear();
        _dbSet.Update(entity);
        
        foreach (var indexType in indexTypes)
        {
            var databaseIndexType = new DataBaseIndexType { DataBaseId = entity.Id, IndexTypeId = indexType.Id };
            await _context.AddAsync(databaseIndexType);
        }
        await _context.SaveChangesAsync();
    }
}