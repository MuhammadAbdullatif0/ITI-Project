using API;
using API.Controllers;
using API.Dtos;
using AutoMapper;
using Core;
using Core.Interfaces;
using Core.Specifiction;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepo<Product> products;
    private readonly IGenericRepo<ProductBrand> brand;
    private readonly IGenericRepo<ProductType> type;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public ProductsController(
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
    [HttpPatch("{id}")]
    public async Task<ActionResult> updateProduct(int id, [FromForm] API.Dtos.ProductToUpdate product)
    {
        var FoundProduct = await unitOfWork.Repository<Core.Product>().GetByIdAsync(id);

        FoundProduct.Description = product.Description;
        FoundProduct.Name = product.Name;
        FoundProduct.Price = product.Price;
       

        if (product.ImgUrl is not null)
        {
            var extension = Path.GetExtension(product.ImgUrl.FileName);

            var newName = $"{DateTime.Now.Ticks}{extension}";

            var ourDirectory = Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", newName);

            using (var stream = new System.IO.FileStream(ourDirectory, FileMode.Create))
            {
                product.ImgUrl.CopyTo(stream);
            }
            FoundProduct.imgUrl = "images/products/" + newName;

        }



       // products.Update(FoundProduct);
        unitOfWork.Repository<Product>().Update(FoundProduct);
        var result = await unitOfWork.Complete();

        if (result <= 0) return NotFound();


        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromForm] ProductToAddDto product)
    {
        var FoundProduct = new Product();

        FoundProduct.Description = product.Description;
        FoundProduct.Name = product.Name;
        FoundProduct.Price = product.Price;
        FoundProduct.ProductBrandId = product.ProductBrand;
        FoundProduct.ProductTypeId = product.ProductType;

        if (product.ImgUrl is not null)
        {
            var extension = Path.GetExtension(product.ImgUrl.FileName);

            var newName = $"{DateTime.Now.Ticks}{extension}";

            var ourDirectory = Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", newName);

            using (var stream = new System.IO.FileStream(ourDirectory, FileMode.Create))
            {
                product.ImgUrl.CopyTo(stream);
            }
            FoundProduct.imgUrl = "images/products/" + newName;

        }



        products.Add(FoundProduct);

        var result = await unitOfWork.Complete();

        if (result <= 0) return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var FoundProduct = await products.GetByIdAsync(id);
        products.Delete(FoundProduct);
        var result = await unitOfWork.Complete();

        if (result <= 0) return NotFound();
        return Ok();
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
