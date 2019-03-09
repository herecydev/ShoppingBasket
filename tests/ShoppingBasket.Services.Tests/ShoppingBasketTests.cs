using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShoppingBasket.Services.Tests
{
    public class ShoppingBasketTests
    {
        private readonly ShoppingBasket _shoppingBasket;

        public ShoppingBasketTests()
        {
            _shoppingBasket = new ShoppingBasket();
        }

        private static ProductItem CreateProductItem(decimal value)
            => new ProductItem
            {
                Id = Guid.NewGuid().ToString(),
                Value = value
            };

        [Theory]
        [InlineData(10, 20, 30)]
        [InlineData(4, 1, 5)]
        [InlineData(8, 0, 8)]
        public void CanSumItemsWhenAdded(decimal first, decimal second, decimal sum)
        {
            _shoppingBasket.Add(CreateProductItem(first));
            _shoppingBasket.Add(CreateProductItem(second));
            _shoppingBasket.Total().Should().Be(sum);
        }
    }
}
