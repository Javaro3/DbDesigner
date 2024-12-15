using Common.GenerateModels;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.ArchitectureBuilders;

public class RepositoryBuilder : IArchitectureBuilder
{
    private readonly ArchitectureGenerateModel architectureModel = new();
    
    public IArchitectureBuilder AddName()
    {
        architectureModel.Name = "Repository";
        return this;
    }

    public IArchitectureBuilder AddInterfaces()
    {
        const string content = """
                               public interface IRepository<T>
                               {
                                   IQueryable<T> GetAll();
                               
                                   Task<T?> GetByIdAsync(int id);
                               
                                   Task AddAsync(T entity);
                               
                                   Task UpdateAsync(T entity);
                               
                                   Task DeleteAsync(T entity);
                               }
                               """;
        const string name = "IRepository";
        architectureModel.InterfaceFiles.Add((name, content));
        return this;
    }

    public IArchitectureBuilder AddImplementation(OrmGenerateModel ormModel)
    {
        var baseContent = """
                          using Microsoft.EntityFrameworkCore;
                          
                          public class Repository<T> : IRepository<T>
                          {
                              protected readonly ORM_NAME _context;
                              protected readonly DbSet<T> _dbSet;
                              
                              public Repository(ORM_NAME context)
                              {
                                  _context = context;
                                  _dbSet = _context.Set<T>();
                              }
                          
                              public virtual IQueryable<T> GetAll()
                              {
                                  return _dbSet.AsQueryable();
                              }
                          
                              public virtual async Task<T?> GetByIdAsync(int id)
                              {
                                  return await _dbSet.FindAsync(id);
                              }
                          
                              public virtual async Task AddAsync(T entity)
                              {
                                  await _dbSet.AddAsync(entity);
                                  await _context.SaveChangesAsync();
                              }
                          
                              public virtual async Task UpdateAsync(T entity)
                              {
                                  _dbSet.Update(entity);
                                  await _context.SaveChangesAsync();
                              }
                          
                              public virtual async Task DeleteAsync(T entity)
                              {
                                  _dbSet.Remove(entity);
                                  await _context.SaveChangesAsync();
                              }
                          }
                          """;

        var baseName = "Repository";
        baseContent = baseContent.Replace("ORM_NAME", ormModel.OrmName);
        architectureModel.ImplementationFiles.Add((baseName, baseContent));

        foreach (var domain in ormModel.Domains)
        {
            var name = $"{domain}{baseName}";
            var content = """
                          public class REPOSITORY_NAME : Repository<DOMAIN_NAME>
                          {
                              public ArchitectureRepository(ORM_NAME context) : base(context)
                              {
                              }
                          }
                          """;
            content = content.Replace("REPOSITORY_NAME", name);
            content = content.Replace("DOMAIN_NAME", domain);
            content = content.Replace("ORM_NAME", ormModel.OrmName);
            architectureModel.ImplementationFiles.Add((name, content));
        }
        
        return this;
    }
    
    public ArchitectureGenerateModel Generate()
    {
        return architectureModel;
    }
}