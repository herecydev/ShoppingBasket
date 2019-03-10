using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IItemStore
    {
        Task AddItemAsync(Item item);
        Task<Item> GetItemAsync(string id);
        Task RemoveItemAsync(string id);
    }
}
