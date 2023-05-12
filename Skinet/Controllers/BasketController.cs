using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepo basket;

    public BasketController(IBasketRepo basket)
    {
        this.basket = basket;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basketVar = await basket.GetBaskeAsync(id);
        return Ok(basketVar ?? new CustomerBasket(id));

    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Upbasket)
    {
        var updatedBasket = await basket.UpdateBaskeAsync(Upbasket);
        return Ok(updatedBasket);
    }
    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await basket.DeleteBasketAsync(id);
    }

}