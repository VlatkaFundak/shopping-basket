using ShoppingBasket.Service.Infrastructure;
using ShoppingBasket.Service.Infrastructure.Factories;
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
        /// The logger
        /// </summary>
        private readonly IShoppingBasketLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingBasketService"/> class.
        /// </summary>
        /// <param name="basketCalculationService">The basket calculation service.</param>
        internal ShoppingBasketService(IBasketCalculationService basketCalculationService)
        {
            BasketCalculationService = basketCalculationService;
            _logger = LoggerFactory.CreateShoppingLogger();
        }

        /// <summary>
        /// Gets the basket calculation service.
        /// </summary>
        /// <value>
        /// The basket calculation service.
        /// </value>
        internal IBasketCalculationService BasketCalculationService { get; }

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

            shoppingBasket.Total = await BasketCalculationService.CalculateTotalAsync(shoppingBasket.ShoppingBasketItems, shoppingBasket.Discounts);

            //TODO log total
            await _logger.LogTotalAsync(shoppingBasket);
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
                _logger.LogError(message);

                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}