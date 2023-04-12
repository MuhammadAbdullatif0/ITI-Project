using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        // to apply all the entity type configurations that are defined in the current executing assembly. 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

        if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach(var item in modelBuilder.Model.GetEntityTypes())
            {
                var prop = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                foreach (var property in prop)
                {
                    modelBuilder.Entity(item.Name).Property(property.Name).HasConversion<double>();
                }
            }
        }
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<ProductType> Types { get; set; }
}
