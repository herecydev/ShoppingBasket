using ShoppingBasket.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShoppingBasketCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IShoppingBasketProvider, ShoppingBasketService>();
            serviceCollection.AddSingleton<IItemService, ShoppingBasketService>();
            return serviceCollection;
        }

        public static IServiceCollection AddInMemoryStore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IShoppingBasketStore, InMemoryBasketStore>();
            serviceCollection.AddSingleton<IItemStore, InMemoryItemStore>();
            return serviceCollection;
        }
    }
}
