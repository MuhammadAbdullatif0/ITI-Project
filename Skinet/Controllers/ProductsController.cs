using API;
using API.Controllers;
using AutoMapper;
using Core;
using Core.Specifiction;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepo<Product> products;
    private readonly IGenericRepo<ProductBrand> brand;
    private readonly IGenericRepo<ProductType> type;
    private readonly IMapper mapper;

    public ProductsController(
        IGenericRepo<Product> products,
        IGenericRepo<ProductBrand> brand,
        IGenericRepo<ProductType> type,
        IMapper mapper
        )
    {
        this.products = products;
        this.brand = brand;
        this.type = type;
        this.mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParam productParam)
    {
        var spec = new ProductsWithType_BrandSpecification(productParam);
        var count = new ProductWithFiltersForCountForCountSpecifications(productParam);
        var totalItem = await products.CountAsync(count);
        var prod = await products.ListAsync(spec);
        var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(prod);
        return  Ok(new Pagination<ProductToReturnDto>(productParam.PageIndex , productParam.PageSize , totalItem , data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithType_BrandSpecification(id);
        var oneProduct = await products.GetEntityWithSpecification(spec);
        if (oneProduct == null)
            return NotFound(new ApiResponse(404));

        return mapper.Map<Product ,ProductToReturnDto>(oneProduct);

    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
    {
        return Ok(await brand.GetAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
    {
        return Ok(await type.GetAsync());
    }
}
