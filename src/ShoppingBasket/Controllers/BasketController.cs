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

        public BasketController(IShoppingBasketProvider shoppingBasketProvider)
        {
            _shoppingBasketProvider = shoppingBasketProvider;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public Task<string> CreateBasket() =>
            _shoppingBasketProvider.CreateShoppingBasketAsync();

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public Task DeleteBasket(string id)
            => _shoppingBasketProvider.DeleteShoppingBasketAsync(id);
    }
}
