using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Service.Infrastructure;
using ShoppingBasket.Service.Providers;
using ShoppingBasket.Service.Services.ShoppingBasketDetails;
using ShoppingBasket.Service.Services.ShoppingBasketDetails.Contracts;

namespace ShoppingBasket.Service
{
    /// <summary>
    /// Services configuration for add shopping basket service
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Adds the shopping basket services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddShoppingBasketServices(this IServiceCollection services)
        {
            services.AddTransient<IShoppingBasketItemService, ShoppingBasketItemService>();
            services.AddTransient<IShoppingBasketService, ShoppingBasketService>();
            services.AddTransient<IDiscountProvider, ProductQuantityDiscountProvider>();
            services.AddTransient<IDiscountProvider, AnotherProductPercentageDiscountProvider>();
            services.AddTransient<IBasketCalculationService, BasketCalculationService>();
            services.AddTransient<IShoppingBasketLogger, ShoppingBasketLogger>();
        }
    }
}