using System.Collections.Generic;

namespace ShoppingBasket.Services
{
    public class ShoppingBasketResult
    {
        public List<ItemRejection> ItemRejections { get; set; } = new List<ItemRejection>();
        public decimal Total { get; set; }
    };
}
