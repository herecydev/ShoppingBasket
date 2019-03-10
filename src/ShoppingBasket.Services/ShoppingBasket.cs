﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Services
{
    // We've gone for an event store style of shopping basket here, i.e. everything is forward only and immutable
    public class ShoppingBasket : IShoppingBasket
    {
        private ConcurrentBag<ShoppingBasketOperation> _shoppingBasketItems = new ConcurrentBag<ShoppingBasketOperation>();

        public void Add(Item shoppingBasketItem)
        {
            /* There's definitely some potential for race conditions that aren't being covered. We have a few options that depend on architecture
               but most databases will cover the optimistic locking well
            */
            _shoppingBasketItems.Add(new ShoppingBasketOperation
            {
                ShoppingBasketItem = shoppingBasketItem,
                ShoppingBasketOperationType = ShoppingBasketOperationType.Add
            });
        }

        public void Remove(Item shoppingBasketItem)
        {
            _shoppingBasketItems.Add(new ShoppingBasketOperation
            {
                ShoppingBasketItem = shoppingBasketItem,
                ShoppingBasketOperationType = ShoppingBasketOperationType.Remove
            });
        }

        public ShoppingBasketResult Total()
        {
            var categoryCosts = new Dictionary<string, decimal>();

            // Calculate the subtotal of the standard product items first
            foreach (var item in _shoppingBasketItems.Where(x => x.ShoppingBasketItem is ProductItem))
            {
                var productItem = item.ShoppingBasketItem as ProductItem;
                if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Add)
                {
                    categoryCosts.TryGetValue(productItem.ProductType, out var categoryCost);
                    categoryCost += item.ShoppingBasketItem.Value;
                    categoryCosts[productItem.ProductType] = categoryCost;
                }
                else if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Remove)
                {
                    categoryCosts.TryGetValue(productItem.ProductType, out var categoryCost);
                    categoryCost -= item.ShoppingBasketItem.Value;
                    categoryCosts[productItem.ProductType] = categoryCost;
                }
            }

            var subTotal = categoryCosts.Sum(x => x.Value);
            var discount = 0M;
            var offerVoucherCount = 0;
            var itemResults = new List<ItemResult>();

            // Roll through all the resulting vouchers
            foreach (var voucher in GetVouchers())
            {
                if (voucher is GiftVoucher)
                {
                    discount += voucher.Value;
                }
                else if (voucher is OfferVoucher offerVoucher)
                {
                    offerVoucherCount += 1;
                    // We're not allowing more than one offer voucher
                    if (offerVoucherCount > 1)
                    {
                        itemResults.Add(new ItemResult
                        {
                            Item = voucher,
                            ItemResultAction = ItemResultAction.RejectItem,
                            Message = "Another offer voucher has already been applied"
                        });
                    }
                    else
                    {
                        if (subTotal > offerVoucher.Threshold)
                        {
                            if (offerVoucher.ProductType == null)
                            {
                                discount += offerVoucher.Value;
                            }
                            else
                            {
                                var matchingCategory = categoryCosts.TryGetValue(offerVoucher.ProductType, out var categoryCost);
                                if (categoryCost > 0)
                                {
                                    discount += Math.Min(offerVoucher.Value, categoryCost);
                                }
                                else
                                {
                                    itemResults.Add(new ItemResult
                                    {
                                        Item = voucher,
                                        ItemResultAction = ItemResultAction.RejectItem,
                                        Message = $"There are no products in your basket applicable to voucher {voucher.Id}"
                                    });
                                }
                            }
                        }
                    }
                }
            }
            var total = Math.Max(subTotal - discount, 0);

            return new ShoppingBasketResult
            {
                Total = total
            };
        }

        private List<Voucher> GetVouchers()
        {
            var vouchers = new List<Voucher>();
            foreach (var item in _shoppingBasketItems.Where(x => x.ShoppingBasketItem is Voucher))
            {
                var voucher = item.ShoppingBasketItem as Voucher;
                if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Add)
                {
                    vouchers.Add(voucher);
                }
                else if (item.ShoppingBasketOperationType == ShoppingBasketOperationType.Remove)
                {
                    var matchingVoucher = vouchers.First(x => x.Id == item.ShoppingBasketItem.Id);
                    if (matchingVoucher != null)
                        vouchers.Remove(matchingVoucher);
                }
            }

            return vouchers;
        }
    }
}
