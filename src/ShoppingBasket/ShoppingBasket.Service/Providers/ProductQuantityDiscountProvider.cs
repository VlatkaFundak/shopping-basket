using ShoppingBasket.Service.Common.Enums;
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
            discounts = discounts.OrderByDescending(p => p.DiscountQuantity);
            foreach (var item in shoppingBasketItems)
            {
                var discount = discounts.FirstOrDefault(p => p.MainProductId == item.ProductId);
                if (discount is null)
                {
                    continue;
                }

                if (item.Discount is null && item.Quantity >= discount.MainQuantity)
                {
                    int quantityDiscount = (item.Quantity / discount.MainQuantity) * discount.DiscountQuantity;

                    if (item.Quantity < discount.MainQuantity + discount.DiscountQuantity)
                    {
                        quantityDiscount = 1;
                    }
                    else if (item.Quantity <= quantityDiscount)
                    {
                        quantityDiscount = item.Quantity;
                    }

                    discountResult += quantityDiscount * item.Product.Price;
                    item.Discount = discount;
                    item.DiscountAmount = discountResult;
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}