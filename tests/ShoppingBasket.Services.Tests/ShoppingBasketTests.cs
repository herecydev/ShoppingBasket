using FluentAssertions;
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

        public static IEnumerable<object[]> WhenCallingTotalThenAddedItemsAreSummedData =>
          new List<object[]>
          {
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 10, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 20, ShoppingBasketOperationType.Add),
                    },
                    30
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 4, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 1, ShoppingBasketOperationType.Add),
                    },
                    5
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 8, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 0, ShoppingBasketOperationType.Add),
                    },
                    8
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 8, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("1", 8, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 10, ShoppingBasketOperationType.Add),
                    },
                    26
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 10, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("1", 10, ShoppingBasketOperationType.Remove),
                    },
                    0
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 4, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 1, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("1", 4, ShoppingBasketOperationType.Remove),
                    },
                    1
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation("1", 8, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 50, ShoppingBasketOperationType.Add),
                        CreateBasketOperation("2", 50, ShoppingBasketOperationType.Remove),
                    },
                    8
                },
          };

        [Theory]
        [MemberData(nameof(WhenCallingTotalThenAddedItemsAreSummedData))]
        public void WhenCallingTotalThenItemsAreSummed(List<ShoppingBasketOperation> basketOperations, decimal sum)
        {
            ApplyBasketOperations(basketOperations);
            _shoppingBasket.Total().Total.Should().Be(sum);
        }


        private static ProductItem CreateProductItem(string id, decimal value)
            => new ProductItem
            {
                Id = id,
                Value = value,
                IsDiscountable = true,
                ProductType = "Jeans"
            };

        private static ShoppingBasketOperation CreateBasketOperation(string id, decimal value, ShoppingBasketOperationType basketOperationType)
            => new ShoppingBasketOperation
            {
                ShoppingBasketItem = CreateProductItem(id, value),
                ShoppingBasketOperationType = basketOperationType,
            };

        private void ApplyBasketOperations(List<ShoppingBasketOperation> basketOperations)
        {
            foreach (var basketOperation in basketOperations)
            {
                if (basketOperation.ShoppingBasketOperationType == ShoppingBasketOperationType.Add)
                    _shoppingBasket.Add(basketOperation.ShoppingBasketItem);
                else if (basketOperation.ShoppingBasketOperationType == ShoppingBasketOperationType.Remove)
                    _shoppingBasket.Remove(basketOperation.ShoppingBasketItem);
            }
        }
    }
}
