using System;
using System.Collections.Generic;

namespace ShoppingBasket.Services
{
    public class InMemoryItemStoreOptions
    {
        public List<ProductItem> ProductItems { get; set; }
        public List<GiftVoucher> GiftVouchers { get; set; }
        public List<OfferVoucher> OfferVouchers { get; set; }
    }
}
