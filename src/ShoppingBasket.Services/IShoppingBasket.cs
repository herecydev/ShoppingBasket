namespace ShoppingBasket.Services
{
    public interface IShoppingBasket
    {
        void Add(ShoppingBasketItem shoppingBasketItem);
        void Remove(ShoppingBasketItem shoppingBasketItem);
        decimal Total();
    }
}
