using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public class InMemoryBasketStore : IShoppingBasketStore
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<InMemoryBasketStoreOptions> _options;

        public InMemoryBasketStore(IMemoryCache memoryCache, IOptions<InMemoryBasketStoreOptions> options)
        {
            _memoryCache = memoryCache;
            _options = options;
        }

        public async Task<IShoppingBasket> GetOrCreateAsync(string id)
        {
            var shoppingBasket = await _memoryCache.GetOrCreateAsync(GetBasketKey(id), cacheEntry =>
            {
                // A sliding expiration will prevent infinite storage of baskets into memory, we should expect that a POS could crash or be ungracefully abandoned leaving an orphaned basket
                cacheEntry.SlidingExpiration = _options.Value.InactivityTimeout;

                // Task.FromResult isn't ideal but async on the interface makes complete sense in "real world" examples, e.g. database, distributed redis instance, etc.

                /* 
                 We could absolutely use the factory pattern here instead of creating shopping basket directly.
                 A good use case might be that only certain stores permit the use of vouchers (i.e. "Can not use in city center stores"), by passing the store id through to the factory we could
                 swap out which concrete shopping basket we use for one that displays a message to the POS and rejects the voucher
                */
                return Task.FromResult(new ShoppingBasket());
            });
            return shoppingBasket;
        }

        public Task RemoveAsync(string id)
        {
            _memoryCache.Remove(id);
            return Task.CompletedTask;
        }

        public Task UpsertAsync(string id, IShoppingBasket shoppingBasket)
        {
            _memoryCache.Set(GetBasketKey(id), shoppingBasket);
            return Task.CompletedTask;
        }

        private static string GetBasketKey(string id)
            => "ShoppingBasket-" + id;
    }
}
