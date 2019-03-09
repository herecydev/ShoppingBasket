using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IShoppingBasketProvider
    {
        Task<string> CreateShoppingBasketAsync();
        Task DeleteShoppingBasketAsync(string id);
    }
}
