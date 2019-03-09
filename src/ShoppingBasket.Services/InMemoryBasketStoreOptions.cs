using System;

namespace ShoppingBasket.Services
{
    public class InMemoryBasketStoreOptions
    {
        public TimeSpan InactivityTimeout { get; set; } = TimeSpan.FromHours(1);
    }
}
