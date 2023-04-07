using Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure;

public class StoreContext:DbContext
{
    public StoreContext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<ProductType> Types { get; set; }
}
