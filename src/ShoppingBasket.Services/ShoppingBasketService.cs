using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public class ShoppingBasketService : IShoppingBasketProvider, IItemService
    {
        private readonly IShoppingBasketStore _shoppingBasketStore;
        private readonly IItemStore _itemStore;

        public ShoppingBasketService(IShoppingBasketStore shoppingBasketStore, IItemStore itemStore)
        {
            _shoppingBasketStore = shoppingBasketStore;
            _itemStore = itemStore;
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

        public async Task AddShoppingBasketItemAsync(string basketId, string itemId)
        {
            var basket = await _shoppingBasketStore.GetOrCreateAsync(basketId);
            var item = await _itemStore.GetItemAsync(itemId);
            basket.Add(item);
            await _shoppingBasketStore.UpsertAsync(basketId, basket);
        }

        public Task RemoveShoppingBasketItemAsync(string basketId, string itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingBasketResult> GetTotal(string basketId)
        {
            var basket = await _shoppingBasketStore.GetOrCreateAsync(basketId);
            return basket.Total();
        }
    }
}
