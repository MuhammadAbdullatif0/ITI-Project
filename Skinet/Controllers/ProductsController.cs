using Core;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;

namespace Skinet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepo product;

    public ProductsController(IProductRepo product)
    {
        this.product = product;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct()
    {
        var prod = await product.GetAsync();
        return Ok(prod);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var oneProduct = await product.GetByIdAsync(id);
        return Ok(oneProduct);

    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
    {
        return Ok(await product.GetBrandAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
    {
        return Ok(await product.GetTypeAsync());
    }
}
