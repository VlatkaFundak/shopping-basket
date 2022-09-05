using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using ShoppingBasket.Service.Providers;
using ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails
{
    /// <summary>
    /// Basket calculation service
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts.IBasketCalculationService" />
    internal class BasketCalculationService : IBasketCalculationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasketCalculationService"/> class.
        /// </summary>
        /// <param name="discountProviders">The discount providers.</param>
        internal BasketCalculationService(IEnumerable<IDiscountProvider> discountProviders)
        {
            DiscountProviders = discountProviders;
        }

        /// <summary>
        /// Gets or sets the discount providers.
        /// </summary>
        /// <value>
        /// The discount providers.
        /// </value>
        protected IEnumerable<IDiscountProvider> DiscountProviders { get; set; }

        /// <summary>
        /// Calculates the total asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        public async Task<decimal> CalculateTotalAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            var total = await CalculateTotalWithoutDiscuntAsync(shoppingBasketItems);
            var discount = await CalculateBasketDiscountAsync(shoppingBasketItems, discounts);

            return total - discount;
        }

        /// <summary>
        /// Calculates the total without discunt asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <returns></returns>
        protected virtual Task<decimal> CalculateTotalWithoutDiscuntAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems)
        {
            return Task.FromResult(shoppingBasketItems.Sum(p => p.Quantity * p.Product.Price));
        }

        /// <summary>
        /// Calculates the basket discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected virtual async Task<decimal> CalculateBasketDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            var isValid = await ValidateDiscountAsync(discounts);
            if (!isValid)
            {
                return decimal.Zero;
            }

            return await CalculateDiscountAsync(shoppingBasketItems, discounts);
        }

        /// <summary>
        /// Validates the discount asynchronous.
        /// </summary>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        protected virtual Task<bool> ValidateDiscountAsync(IEnumerable<IDiscount> discounts)
        {
            if (discounts is null || !discounts.Any())
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Calculates the discount asynchronous.
        /// </summary>
        /// <param name="shoppingBasketItems">The shopping basket items.</param>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        private async Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            decimal discount = decimal.Zero;

            var groupedDiscounts = discounts.OrderBy(p => p.Priority).GroupBy(p => p.DiscountType);

            foreach (var groupedDiscount in groupedDiscounts)
            {
                var provider = DiscountProviders.First(p => p.DiscountType.HasFlag(groupedDiscount.Key));
                discount += await provider.GetDiscountAsync(shoppingBasketItems, groupedDiscount.ToList());
            }

            return discount;
        }
    }
}