using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    // TODO hate this name
    public interface IItemService
    {
        Task AddShoppingBasketItemAsync(string basketId, string itemId);
        Task RemoveShoppingBasketItemAsync(string basketId, string itemId);
        Task<ShoppingBasketResult> GetTotal(string basketId);
    }
}
