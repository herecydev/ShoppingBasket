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
                        CreateBasketOperation(CreateProductItem("1", 10), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 20), ShoppingBasketOperationType.Add),
                    },
                    30
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 4), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 1), ShoppingBasketOperationType.Add),
                    },
                    5
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 0), ShoppingBasketOperationType.Add),
                    },
                    8
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 10), ShoppingBasketOperationType.Add),
                    },
                    26
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 10), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("1", 10), ShoppingBasketOperationType.Remove),
                    },
                    0
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 4), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 1), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("1", 4), ShoppingBasketOperationType.Remove),
                    },
                    1
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Remove),
                    },
                    8
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateGiftVoucher("000-001", 50), ShoppingBasketOperationType.Add),
                    },
                    8
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 12), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateGiftVoucher("000-001", 50), ShoppingBasketOperationType.Add),
                    },
                    12
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateNonDiscountableProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateGiftVoucher("000-001", 50), ShoppingBasketOperationType.Add),
                    },
                    50
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateNonDiscountableProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateGiftVoucher("000-001", 50), ShoppingBasketOperationType.Add),
                    },
                    8
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateOfferVoucher("000-002", 50, 60), ShoppingBasketOperationType.Add),
                    },
                    58
                },
                new object[] {
                    new List<ShoppingBasketOperation> {
                        CreateBasketOperation(CreateProductItem("1", 8), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateProductItem("2", 50), ShoppingBasketOperationType.Add),
                        CreateBasketOperation(CreateOfferVoucher("000-002", 50, 50), ShoppingBasketOperationType.Add),
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

        private static ProductItem CreateNonDiscountableProductItem(string id, decimal value)
            => new ProductItem
            {
                Id = id,
                Value = value,
                IsDiscountable = false,
                ProductType = "Jeans"
            };

        private static GiftVoucher CreateGiftVoucher(string id, decimal value)
            => new GiftVoucher
            {
                Id = id,
                Value = value
            };

        private static OfferVoucher CreateOfferVoucher(string id, decimal value, decimal threshold)
            => new OfferVoucher
            {
                Id = id,
                Value = value,
                Threshold = threshold
            };

        private static ShoppingBasketOperation CreateBasketOperation(Item item, ShoppingBasketOperationType basketOperationType)
            => new ShoppingBasketOperation
            {
                ShoppingBasketItem = item,
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
