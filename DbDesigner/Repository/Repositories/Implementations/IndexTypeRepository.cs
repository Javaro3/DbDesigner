using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class IndexTypeRepository : Repository<IndexType>
{
    private readonly Expression<Func<IndexType, IndexType>> _selector = e => new IndexType
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        DataBases = e.DataBases.Select(db => new DataBase
        {
            Id = db.Id,
            Name = db.Name,
            Description = db.Description,
            Image = db.Image
        }).ToList()
    };
    
    public IndexTypeRepository(DbDesignerContext context) : base(context)
    {
    }

    public override IQueryable<IndexType> GetAll()
    {
        return _dbSet.Include(e => e.DataBases)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<IndexType?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.DataBases)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public override async Task UpdateAsync(IndexType entity)
    {
        var databaseIndexTypes = _context.DataBaseIndexTypes.Where(e => e.IndexTypeId == entity.Id).ToList();
        _context.RemoveRange(databaseIndexTypes);
        
        var dataBases = entity.DataBases.ToList();
        entity.DataBases.Clear();
        _dbSet.Update(entity);
        
        foreach (var dataBase in dataBases)
        {
            var databaseIndexType = new DataBaseIndexType { IndexTypeId = entity.Id, DataBaseId = dataBase.Id };
            await _context.AddAsync(databaseIndexType);
        }
        await _context.SaveChangesAsync();
    }
}