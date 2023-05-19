namespace Core;

public class ProductsWithType_BrandSpecification:BaseSpecification<Product>
{
    public ProductsWithType_BrandSpecification(ProductSpecParam param)
        :base(x =>
        (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
        (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) && 
        (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId))
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        ADDOrderBy(x => x.Name);
        ApplyPaging(param.PageSize * (param.PageIndex - 1) , param.PageSize);

        if (!string.IsNullOrEmpty(param.Sort))
        {
            switch (param.Sort)
            {
                case "priceAsc":
                    ADDOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    ADDOrderByDesc(x => x.Price);
                    break;
                default:
                    ADDOrderBy(x => x.Name);
                    break;
            }
        }
    }

    public ProductsWithType_BrandSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
