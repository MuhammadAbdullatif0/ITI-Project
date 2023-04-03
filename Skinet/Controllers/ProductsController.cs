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
    private readonly StoreContext context;

    public ProductsController(StoreContext context)
    {
        this.context = context;
    }
    [HttpGet]
    public  async Task<ActionResult<List<Product>>> GetProduct()
    {
        return await context.products.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await context.products.FindAsync(id);
        
    }
}
