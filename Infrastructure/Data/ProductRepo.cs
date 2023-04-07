using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ProductRepo : IProductRepo
{
    private readonly StoreContext context;

    public ProductRepo(StoreContext context)
    {
        this.context = context;
    }
    public async Task<IReadOnlyList<Product>> GetAsync()
    {
        return await context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrandAsync()
    {
        return await context.Brands.ToListAsync();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return context.Products
            .Include(p=>p.ProductBrand)
            .Include(p=>p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<ProductType>> GetTypeAsync()
    {
        return await context.Types.ToListAsync();
    }
}
