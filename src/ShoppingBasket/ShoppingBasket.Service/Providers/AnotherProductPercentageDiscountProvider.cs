﻿using ShoppingBasket.Service.Common.Enums;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Providers
{
    /// <summary>
    /// Another prodcut percentage discount provider
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Providers.IDiscountProvider" />
    public class AnotherProductPercentageDiscountProvider : IDiscountProvider
    {
        /// <summary>
        /// Gets the type of the discount.
        /// </summary>
        /// <value>
        /// The type of the discount.
        /// </value>
        public DiscountType DiscountType => DiscountType.AnotherProductPercentage;

        /// <summary>
        /// Gets the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        public Task<decimal> GetDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            //TODO maybe change this, there is a dillema should I leave everything in one model, or as now is, separate ones, and cast
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IAnotherProductPercentageDiscount>());
        }

        /// <summary>
        /// Calculates the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected virtual Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IAnotherProductPercentageDiscount> discounts)
        {
            decimal discountResult = decimal.Zero;

            foreach (var item in shoppingBasketItems)
            {
                var discount = discounts.OrderByDescending(p => p.DiscountQuantity).FirstOrDefault(p => p.MainProductId.Equals(item.ProductId));
                if (discount is null)
                {
                    continue;
                }

                var discountProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.DiscountProductId);
                if (discountProduct is null || discountProduct.Discount is not null)
                {
                    continue;
                }

                var quantityDiscount = (int)Math.Floor(item.Quantity / discount.MainQuantity);

                var discountPrice = (discount.Percentage / 100m) * discountProduct.Product.Price;

                if (discountProduct.Quantity <= quantityDiscount)
                {
                    discountResult += discountProduct.Quantity * discountPrice;
                }
                else
                {
                    discountResult += quantityDiscount * discountPrice;
                }

                discountProduct.Discount = discount;
                discountProduct.DiscountAmount = discountResult;
            }

            return Task.FromResult(discountResult);
        }
    }
}