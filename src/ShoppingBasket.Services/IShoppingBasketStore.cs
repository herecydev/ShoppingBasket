using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IShoppingBasketStore
    {
        Task<IShoppingBasket> GetOrCreateAsync(string id);
        Task RemoveAsync(string id);
    }
}
