using Core;
using Core.Entities.OrderAggregate;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Brands.Any())
        {
            var BrandData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
            context.Brands.AddRange(brands);
        }

        if (!context.Types.Any())
        {
            var TypeData = File.ReadAllText("../Infrastructure/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
            context.Types.AddRange(types);
        }

        if (!context.Products.Any())
        {
            var ProductData = File.ReadAllText("../Infrastructure/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            context.Products.AddRange(products);
        }

        
        if (!context.DeliveryMethods.Any())
        {
            var deliveryData = File.ReadAllText("../Infrastructure/SeedData/delivery.json");
            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            context.DeliveryMethods.AddRange(methods);
        }

        if (context.ChangeTracker.HasChanges())
            await context.SaveChangesAsync();
    }
}

