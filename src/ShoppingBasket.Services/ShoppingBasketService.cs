using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public class ShoppingBasketService : IShoppingBasketProvider
    {
        private readonly IShoppingBasketStore _shoppingBasketStore;

        public ShoppingBasketService(IShoppingBasketStore shoppingBasketStore)
        {
            _shoppingBasketStore = shoppingBasketStore;
        }

        public async Task<string> CreateShoppingBasketAsync()
        {
            var id = Guid.NewGuid().ToString();
            await _shoppingBasketStore.GetOrCreateAsync(id);
            return id;
        }

        public async Task DeleteShoppingBasketAsync(string id)
        {
            await _shoppingBasketStore.RemoveAsync(id);
        }
    }
}
