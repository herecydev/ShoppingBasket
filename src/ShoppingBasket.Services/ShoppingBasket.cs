using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Services
{
    public class ShoppingBasket : IShoppingBasket
    {
        private Dictionary<string, ShoppingBasketItem> _items = new Dictionary<string, ShoppingBasketItem>();

        public void Add(ShoppingBasketItem shoppingBasketItem)
            => _items.Add(shoppingBasketItem.Id, shoppingBasketItem);

        public void Remove(ShoppingBasketItem shoppingBasketItem)
            => _items.Remove(shoppingBasketItem.Id);

        public decimal Total()
            => _items.Values.Sum(item => item.Value);
    }
}
