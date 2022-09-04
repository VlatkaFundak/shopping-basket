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
                return 0m;
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

            var providers = await GetActiveDiscountProviderAsync(discounts);

            if (providers?.Any() == true && discounts?.Any() == true)
            {
                var discountTasks = new List<Task<decimal>>();
                foreach (var provider in providers)
                {
                    var matchingDiscounts = discounts.Where(m => m.DiscountType == provider.DiscountType).ToList();
                    discountTasks.Add(provider.GetDiscountAsync(shoppingBasketItems, matchingDiscounts));
                }

                var results = await Task.WhenAll(discountTasks);

                discount = results.Sum();
            }

            return discount;
        }

        /// <summary>
        /// Gets the active discount provider asynchronous.
        /// </summary>
        /// <param name="discounts">The discounts.</param>
        /// <returns></returns>
        private Task<IEnumerable<IDiscountProvider>> GetActiveDiscountProviderAsync(IEnumerable<IDiscount> discounts)
        {
            var selectedDiscountProviders = new List<IDiscountProvider>();
            var oneDiscount = discounts.FirstOrDefault(p => p.ExcludeOtherDiscounts);
            if (oneDiscount is not null)
            {
                selectedDiscountProviders.Add(DiscountProviders.First(p => p.DiscountType.HasFlag(oneDiscount.DiscountType))); //this should throw exception if discount type does not exist in providers - not in try/catch block intentionaly

                return Task.FromResult(selectedDiscountProviders.AsEnumerable());
            }

            foreach (var discount in discounts)
            {
                selectedDiscountProviders.Add(DiscountProviders.First(p => p.DiscountType.HasFlag(discount.DiscountType)));
            }

            return Task.FromResult(selectedDiscountProviders.AsEnumerable());
        }
    }
}