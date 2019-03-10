using System.Collections.Concurrent;

namespace ShoppingBasket.Services
{
    // We've gone for an event store style of shopping basket here, i.e. everything is forward only and immutable
    public class ShoppingBasket : IShoppingBasket
    {
        private ConcurrentBag<ShoppingBasketOperation> _shoppingBasketItems = new ConcurrentBag<ShoppingBasketOperation>();

        public ItemResult Add(Item shoppingBasketItem)
        {
            /* There's definitely some potential for race conditions that aren't being covered. We have a few options that depend on architecture
               but most databases will cover the optimistic locking well
            */
            _shoppingBasketItems.Add(new ShoppingBasketOperation
            {
                ShoppingBasketItem = shoppingBasketItem,
                ShoppingBasketOperationType = ShoppingBasketOperationType.Add
            });

            return new ItemResult
            {
                ItemResultAction = ItemResultAction.Success
            };
        }

        public ItemResult Remove(Item shoppingBasketItem)
        {
            _shoppingBasketItems.Add(new ShoppingBasketOperation
            {
                ShoppingBasketItem = shoppingBasketItem,
                ShoppingBasketOperationType = ShoppingBasketOperationType.Remove
            });

            return new ItemResult
            {
                ItemResultAction = ItemResultAction.Success
            };
        }

        public decimal Total()
        {
            var total = 0M;
            foreach (var item in _shoppingBasketItems)
            {
                if (item.ShoppingBasketItem is ProductItem)
                {
                    if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Add)
                        total += item.ShoppingBasketItem.Value;
                    else if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Remove)
                        total -= item.ShoppingBasketItem.Value;
                }
                else if (item.ShoppingBasketItem is GiftVoucher)
                {
                    total -= item.ShoppingBasketItem.Value;
                }
            }
            return total;
        }
    }
}
