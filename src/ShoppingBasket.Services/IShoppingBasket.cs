namespace ShoppingBasket.Services
{
    public interface IShoppingBasket
    {
        ItemResult Add(Item shoppingBasketItem);
        ItemResult Remove(Item shoppingBasketItem);
        decimal Total();
    }
}
