using ShoppingBasket.Service.Infrastructure;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Factories;
using ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service.Services.ShoppingBasketDetails
{
    /// <summary>
    /// Shopping basket service
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Services.BaseService" />
    /// <seealso cref="ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts.IShoppingBasketService" />
    public class ShoppingBasketService : BaseService, IShoppingBasketService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingBasketService" /> class.
        /// </summary>
        /// <param name="basketCalculationService">The basket calculation service.</param>
        /// <param name="logger">The logger.</param>
        internal ShoppingBasketService(IBasketCalculationService basketCalculationService,
            IShoppingBasketLogger logger)
        {
            BasketCalculationService = basketCalculationService;
            Logger = logger;
        }

        /// <summary>
        /// Gets the basket calculation service.
        /// </summary>
        /// <value>
        /// The basket calculation service.
        /// </value>
        internal IBasketCalculationService BasketCalculationService { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        internal IShoppingBasketLogger Logger { get; }

        /// <summary>
        /// Creates the shopping basket asynchronous.
        /// </summary>
        /// <param name="userIdentifier">The user identifier.</param>
        /// <returns></returns>
        public async Task<IShoppingBasket> CreateShoppingBasketAsync(string userIdentifier)
        {
            var basket = ShoppingBasketFactory.CreateShoppingBasket();
            basket.UserIdentifier = userIdentifier;
            await InitializeAsync(basket);

            return basket;
        }

        /// <summary>
        /// Calculates the basket total asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        public async Task CalculateBasketTotalAsync(IShoppingBasket shoppingBasket)
        {
            var isValid = await ValidateBasketAsync(shoppingBasket);
            if (!isValid)
            {
                shoppingBasket.Total = decimal.Zero;
                return;
            }

            // there is a dillema here should I fetch discounts per product, or get them all and handle discounts per product
            // stayed with this approach because I have discounts per priority, so there can only be one priority discount per product
            shoppingBasket.Total = await BasketCalculationService.CalculateTotalAsync(shoppingBasket.ShoppingBasketItems, shoppingBasket.Discounts);

            await Logger.LogTotalAsync(shoppingBasket);
        }

        /// <summary>
        /// Validates the basket asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">There are no items in the basket to calculate total. Please add items to cart first.</exception>
        protected virtual Task<bool> ValidateBasketAsync(IShoppingBasket shoppingBasket)
        {
            if (shoppingBasket is null || shoppingBasket.ShoppingBasketItems is null || !shoppingBasket.ShoppingBasketItems.Any())
            {
                string message = "There are no items in the basket to calculate total. Please add items to cart first.";
                Logger.LogErrorAsync(message);

                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}