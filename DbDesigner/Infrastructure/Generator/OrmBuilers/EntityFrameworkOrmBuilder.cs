using Common.Extensions;
using Common.GenerateModels;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.OrmBuilers;

public class EntityFrameworkOrmBuilder : IOrmBuilder
{
    private string _code = string.Empty;
    
    public IOrmBuilder AddOrm(OrmGenerateModel model)
    {
        _code = $"""
                using Microsoft.EntityFrameworkCore;
                
                public class {model.OrmName} : DbContext
                
                """;
        _code += "{\n";
        foreach (var domain in model.Domains)
        {
            _code += $"\tpublic virtual DbSet<{domain}> {domain.ToPlural()} {{ get; set; }}\n\n";
        }

        _code += $"\tpublic {model.OrmName}(DbContextOptions<{model.OrmName}> options) : base(options) {{}}\n";
        _code += "}";
        return this;
    }
    
    public string Generate()
    {
        return _code;
    }
}