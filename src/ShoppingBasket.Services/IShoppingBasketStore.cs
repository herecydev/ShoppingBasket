using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IShoppingBasketStore
    {
        Task<IShoppingBasket> GetOrCreateAsync(string id);
        Task UpsertAsync(string id, IShoppingBasket shoppingBasket);
        Task RemoveAsync(string id);
    }
}
