﻿using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    // Using a simple in memory store, a more realistic scenario would be to use either a database or CMS like prismic/umbraco
    public class InMemoryItemStore : IItemStore
    {
        private ConcurrentDictionary<string, Item> _items = new ConcurrentDictionary<string, Item>();

        public InMemoryItemStore(IOptions<InMemoryItemStoreOptions> options)
        {
            foreach (var item in options.Value.ProductItems)
            {
                _items.TryAdd(item.Id, item);
            }

            foreach (var item in options.Value.GiftVouchers)
            {
                _items.TryAdd(item.Id, item);
            }

            foreach (var item in options.Value.OfferVouchers)
            {
                _items.TryAdd(item.Id, item);
            }
        }

        public Task AddItemAsync(Item item)
        {
            _items.TryAdd(item.Id, item);
            return Task.CompletedTask;
        }

        public Task<Item> GetItemAsync(string id)
        {
            _items.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task RemoveItemAsync(string id)
        {
            _items.TryRemove(id, out var _);
            return Task.CompletedTask;
        }
    }
}
