using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Services;
using System.Threading.Tasks;

namespace ShoppingBasket.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IShoppingBasketProvider _shoppingBasketProvider;
        private readonly IItemService _itemService;

        public BasketController(IShoppingBasketProvider shoppingBasketProvider, IItemService itemService)
        {
            _shoppingBasketProvider = shoppingBasketProvider;
            _itemService = itemService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public Task<string> CreateBasket() =>
            _shoppingBasketProvider.CreateShoppingBasketAsync();

        [HttpDelete("{basketId}")]
        [ProducesResponseType(204)]
        public Task DeleteBasket(string basketId)
            => _shoppingBasketProvider.DeleteShoppingBasketAsync(basketId);

        [HttpPost("{basketId}/{itemId}")]
        public Task AddItem(string basketId, string itemId)
            => _itemService.AddShoppingBasketItemAsync(basketId, itemId);

        [HttpDelete("{basketId}/{itemId}")]
        public Task RemoveItem(string basketId, string itemId)
            => _itemService.RemoveShoppingBasketItemAsync(basketId, itemId);

        [HttpGet("{basketId}")]
        public Task<ShoppingBasketResult> GetTotal(string basketId)
            => _itemService.GetTotal(basketId);
    }
}
