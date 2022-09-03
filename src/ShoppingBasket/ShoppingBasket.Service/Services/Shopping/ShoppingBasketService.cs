using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Factories;

namespace ShoppingBasket.Service.Services.Shopping
{
    public class ShoppingBasketService : BaseService, IShoppingBasketService
    {
        public ShoppingBasketService(IBasketCalculationService basketCalculationService)
        {
            BasketCalculationService = basketCalculationService;
        }

        protected IBasketCalculationService BasketCalculationService { get; }

        public async Task<IShoppingBasket> CreateShoppingBasketAsync(string userIdentifier) //TODO maybe remove?
        {
            var basket = ShoppingBasketFactory.CreateShoppingBasket();
            basket.UserIdentifier = userIdentifier;
            await InitializeAsync(basket);

            return basket;
        }

        public async Task CalculateBasketTotalAsync(IShoppingBasket shoppingBasket)
        {
            await ValidateBasketAsync(shoppingBasket);

            shoppingBasket.Total = await BasketCalculationService.CalculateTotalAsync(shoppingBasket.ShoppingBasketItems, shoppingBasket.Discounts);

            //TODO log total
        }

        private Task ValidateBasketAsync(IShoppingBasket shoppingBasket)
        {
            if (shoppingBasket is null || shoppingBasket.ShoppingBasketItems is null || !shoppingBasket.ShoppingBasketItems.Any())
            {
                throw new ArgumentException("There are no items in the basketto calculate total. Please add items to cart first.");
            }

            return Task.CompletedTask;
        }
    }
}