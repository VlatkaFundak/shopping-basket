using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Providers;

namespace ShoppingBasket.Service.Services.Shopping
{
    public class BasketCalculationService : IBasketCalculationService
    {
        public BasketCalculationService(IDiscountProvider[] discountProviders)
        {
            DiscountProviders = discountProviders;
        }

        protected IEnumerable<IDiscountProvider> DiscountProviders { get; set; }

        protected Task<decimal> CalculateTotalWithoutDiscuntAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems)
        {
            return Task.FromResult(shoppingBasketItems.Sum(p => p.Quantity * p.Product.Price));
        }

        public async Task<decimal> CalculateTotalAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            var total = await CalculateTotalWithoutDiscuntAsync(shoppingBasketItems);
            var discount = await CalculateBasketDiscountAsync(shoppingBasketItems, discounts);

            return total - discount;
        }

        protected virtual async Task<decimal> CalculateBasketDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            var isValid = await ValidateDiscountAsync(discounts);
            if (!isValid)
            {
                return 0m;
            }

            return await CalculateDiscountAsync(shoppingBasketItems, discounts);
        }

        protected virtual Task<bool> ValidateDiscountAsync(IEnumerable<IDiscount> discounts)
        {
            if (discounts is null || !discounts.Any())
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        private async Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            decimal discount = decimal.Zero;

            var providers = await GetActiveDiscountProviderAsync(discounts);

            if (providers != null && providers.Any())
            {
                var discountTasks = new List<Task<decimal>>();
                foreach (var provider in providers)
                {
                    var matchingDiscounts = discounts.Where(m => m.DiscountType == provider.DiscountType).ToList();
                    if (discounts.Any())
                    {
                        discountTasks.Add(provider.GetDiscountAsync(shoppingBasketItems, matchingDiscounts));
                    }
                }

                if (discountTasks is not null && discountTasks.Any())
                {
                    var results = await Task.WhenAll(discountTasks);

                    discount = results.Sum();
                }
            }

            return discount;
        }

        private Task<IEnumerable<IDiscountProvider>> GetActiveDiscountProviderAsync(IEnumerable<IDiscount> discounts)
        {
            var selectedDiscountProviders = new List<IDiscountProvider>();
            var oneDiscount = discounts.FirstOrDefault(p => p.ExcludeOtherDiscounts);
            if (oneDiscount is not null)
            {
                selectedDiscountProviders.Add(DiscountProviders.First(p => p.DiscountType.HasFlag(oneDiscount.DiscountType))); //this should throw exception if discount type does not exist in providers

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