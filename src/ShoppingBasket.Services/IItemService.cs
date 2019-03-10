using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    // TODO hate this name
    public interface IItemService
    {
        Task<ItemResult> AddShoppingBasketItemAsync(string basketId, string itemId);
        Task<ItemResult> RemoveShoppingBasketItemAsync(string basketId, string itemId);
        Task<decimal> GetTotal(string basketId);
    }
}
