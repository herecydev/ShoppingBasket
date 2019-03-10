using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Services;
using System.Threading.Tasks;

namespace ShoppingBasket.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemStore _itemStore;

        public ItemsController(IItemStore itemStore)
        {
            _itemStore = itemStore;
        }

        [HttpPost("productitem")]
        [ProducesResponseType(204)]
        public Task AddProductItem([FromBody]ProductItem item) =>
            _itemStore.AddItemAsync(item);

        [HttpPost("giftvoucher")]
        [ProducesResponseType(204)]
        public Task AddGiftVoucher([FromBody]GiftVoucher item) =>
            _itemStore.AddItemAsync(item);

        [HttpPost("offervoucher")]
        [ProducesResponseType(204)]
        public Task AddOfferVoucher([FromBody]OfferVoucher item) =>
            _itemStore.AddItemAsync(item);
    }
}
