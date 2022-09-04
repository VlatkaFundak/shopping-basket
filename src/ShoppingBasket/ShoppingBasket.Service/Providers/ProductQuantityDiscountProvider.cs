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
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IProductQuantityDiscount>());
        }

        /// <summary>
        /// Calculates the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected internal Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IProductQuantityDiscount> discounts)
        {
            decimal discountResult = 0m;

            foreach (var discount in discounts)
            {
                var mainProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.MainProductId);
                if (mainProduct is not null && mainProduct.Quantity >= discount.MainQuantity)
                {
                    var quantityDiscount = (int)Math.Floor(mainProduct.Quantity / (discount.MainQuantity + discount.DiscountQuantity));

                    if (mainProduct.Quantity <= quantityDiscount)
                    {
                        discountResult += mainProduct.Quantity * mainProduct.Product.Price;
                    }

                    if (mainProduct.Quantity > quantityDiscount)
                    {
                        discountResult += quantityDiscount * mainProduct.Product.Price;
                    }
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}