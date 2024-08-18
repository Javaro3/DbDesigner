using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class DbDesignerContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    
    public DbDesignerContext(DbContextOptions<DbDesignerContext> options) : base(options)
    {
        
    }
}