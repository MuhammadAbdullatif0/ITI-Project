using Core.Entities;

namespace Core.Interfaces;

public interface IBasketRepo
{
    Task<CustomerBasket> GetBaskeAsync(string basketId);
    Task<CustomerBasket> UpdateBaskeAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);

}
