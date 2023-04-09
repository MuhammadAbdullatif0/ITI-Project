using System.Linq.Expressions;

namespace Core;

public class ProductsWithType_BrandSpecification:BaseSpecification<Product>
{
    public ProductsWithType_BrandSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }

    public ProductsWithType_BrandSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
