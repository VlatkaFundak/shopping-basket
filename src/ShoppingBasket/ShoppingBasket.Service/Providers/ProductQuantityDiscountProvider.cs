﻿using ShoppingBasket.Service.Common.Enums;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Providers
{
    /// <summary>
    /// Product quantity discount provider
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Providers.IDiscountProvider" />
    public class ProductQuantityDiscountProvider : IDiscountProvider
    {
        /// <summary>
        /// Gets the type of the discount.
        /// </summary>
        /// <value>
        /// The type of the discount.
        /// </value>
        public DiscountType DiscountType => DiscountType.ProductQuantity;

        /// <summary>
        /// Gets the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        public Task<decimal> GetDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            //TODO maybe change this, there is a dillema should I leave everything in one model, or as now is, separate ones, and cast
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IProductQuantityDiscount>());
        }

        /// <summary>
        /// Calculates the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected virtual Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IProductQuantityDiscount> discounts)
        {
            decimal discountResult = decimal.Zero;

            foreach (var item in shoppingBasketItems)
            {
                var discount = discounts.OrderByDescending(p => p.DiscountQuantity).FirstOrDefault(p => p.MainProductId == item.ProductId);
                if (discount is null)
                {
                    continue;
                }

                if (item.Discount is null && item.Quantity >= discount.MainQuantity)
                {
                    var quantityDiscount = (int)Math.Floor(item.Quantity / (discount.MainQuantity + discount.DiscountQuantity)) * discount.DiscountQuantity;

                    if (item.Quantity <= quantityDiscount)
                    {
                        discountResult += item.Quantity * item.Product.Price;
                    }
                    else
                    {
                        discountResult += quantityDiscount * item.Product.Price;
                    }

                    item.Discount = discount;
                    item.DiscountAmount = discountResult;
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}