namespace ShoppingBasket.Services
{
    public interface IShoppingBasket
    {
        void Add(Item shoppingBasketItem);
        void Remove(Item shoppingBasketItem);
        ShoppingBasketResult Total();
    }
}
