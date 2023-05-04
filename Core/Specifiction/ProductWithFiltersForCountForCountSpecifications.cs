namespace Core.Specifiction
{
    public class ProductWithFiltersForCountForCountSpecifications:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountForCountSpecifications(ProductSpecParam param)
        : base(x =>
        (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
        (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) && 
        (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId))
        {
        
        }
    }
}
