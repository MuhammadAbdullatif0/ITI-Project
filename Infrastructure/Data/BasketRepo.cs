using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data;

public class BasketRepo : IBasketRepo
{
    private readonly IDatabase _database;

    public BasketRepo(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
         return await _database.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasket> GetBaskeAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBaskeAsync(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        if (!created)
            return null;
        return await GetBaskeAsync(basket.Id);
    }
}
