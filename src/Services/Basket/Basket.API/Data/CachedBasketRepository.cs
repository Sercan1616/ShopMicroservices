namespace Basket.API.Data;


// Primary Constructor aracılığıyla alınan basketRepository nesnesi, CachedBasketRepository içinde veri tabanı işlemleri veya temel işlevler için kullanılır.
// Örnek return await basketRepository.GetBasket(userName, cancellationToken);

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
{

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            // Json ile serialize edilmiş cachedBasket string'ini ShoppingCart nesnesine deserialize ediyoruz.
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket = await basketRepository.GetBasket(userName, cancellationToken);

        // Eğer basket null değilse, distributedCache üzerine serialize edilmiş haliyle kaydediyoruz.
        await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {

        await basketRepository.StoreBasket(basket, cancellationToken);

        await distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }


    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);

        await distributedCache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
