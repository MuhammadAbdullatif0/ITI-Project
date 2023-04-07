namespace Core;

public interface IProductRepo
{
    Task<Product?> GetByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetAsync();
    Task<IReadOnlyList<ProductBrand>> GetBrandAsync();
    Task<IReadOnlyList<ProductType>> GetTypeAsync();
}
