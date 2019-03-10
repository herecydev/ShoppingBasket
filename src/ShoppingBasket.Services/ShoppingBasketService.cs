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

        public async Task<ItemResult> AddShoppingBasketItemAsync(string basketId, string itemId)
        {
            var basket = await _shoppingBasketStore.GetOrCreateAsync(basketId);
            var item = await _itemStore.GetItemAsync(itemId);

            if (item == null)
                return new ItemResult
                {
                    ItemResultAction = ItemResultAction.RejectItem,
                    Message = "Unrecognized item"
                };
            var result = basket.Add(item);
            await _shoppingBasketStore.UpsertAsync(basketId, basket);
            return result;
        }

        public Task<ItemResult> RemoveShoppingBasketItemAsync(string basketId, string itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTotal(string basketId)
        {
            var basket = await _shoppingBasketStore.GetOrCreateAsync(basketId);
            return basket.Total();
        }
    }
}
