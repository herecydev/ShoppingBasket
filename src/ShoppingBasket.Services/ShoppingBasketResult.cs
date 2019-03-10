using System.Collections.Generic;

namespace ShoppingBasket.Services
{
    public class ShoppingBasketResult
    {
        public List<ItemResult> ItemResults { get; set; } = new List<ItemResult>();
        public decimal Total { get; set; }
    };
}
