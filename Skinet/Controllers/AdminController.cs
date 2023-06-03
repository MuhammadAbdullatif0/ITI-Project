using AutoMapper;
using Core.Specifiction;
using Core;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using static StackExchange.Redis.Role;

namespace API.Controllers
{
    public class AdminController :BaseApiController
    {
        private readonly IGenericRepo<Product> products;
        private readonly IGenericRepo<ProductBrand> brand;
        private readonly IGenericRepo<ProductType> type;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public AdminController(
       IGenericRepo<Product> products,
       IGenericRepo<ProductBrand> brand,
       IGenericRepo<ProductType> type,
       IMapper mapper,
       IUnitOfWork unitOfWork
       )
        {
            this.products = products;
            this.brand = brand;
            this.type = type;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] AdminSpecParams productParam)
        {
            var spec = new ProductsWithType_BrandSpecification(productParam);
            var count = new ProductWithFiltersForCountForCountSpecifications(productParam);
            var totalItem = await products.CountAsync(count);
            var prod = await products.ListAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(prod);
            return Ok(new Pagination<ProductToReturnDto>(productParam.PageIndex, productParam.PageSize, totalItem, data));
        }
    }
}
