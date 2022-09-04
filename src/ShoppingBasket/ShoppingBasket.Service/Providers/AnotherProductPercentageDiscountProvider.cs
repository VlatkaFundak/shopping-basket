using ShoppingBasket.Service.Common.Enums;
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
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IAnotherProductPercentageDiscount>());
        }

        /// <summary>
        /// Calculates the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected internal Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IAnotherProductPercentageDiscount> discounts)
        {
            decimal discountResult = 0m;

            foreach (var discount in discounts)
            {
                var mainProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.MainProductId);
                if (mainProduct is not null && mainProduct.Quantity >= discount.MainQuantity)
                {
                    var discountProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.DiscountProductId);
                    if (discountProduct is null)
                    {
                        return Task.FromResult(0m);
                    }

                    var quantityDiscount = (int)Math.Floor(mainProduct.Quantity / discount.MainQuantity);

                    var discountPrice = (discount.Percentage / 100m) * discountProduct.Product.Price;

                    if (discountProduct.Quantity <= quantityDiscount)
                    {
                        discountResult += discountProduct.Quantity * discountPrice;
                    }

                    if (discountProduct.Quantity > quantityDiscount)
                    {
                        discountResult += quantityDiscount * discountPrice;
                    }
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}