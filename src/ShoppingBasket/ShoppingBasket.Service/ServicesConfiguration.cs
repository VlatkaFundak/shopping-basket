using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Service.Infrastructure;
using ShoppingBasket.Service.Models.Discounts.Factories;
using ShoppingBasket.Service.Providers;
using ShoppingBasket.Service.Services.Shopping;

namespace ShoppingBasket.Service
{
    public static class ServicesConfiguration
    {
        public static void AddShoppingBasketServices(this IServiceCollection services)
        {
            services.AddTransient<IShoppingBasketItemService, ShoppingBasketItemService>();
            services.AddTransient<IShoppingBasketService, ShoppingBasketService>();

            services.AddSingleton<IDiscountProvider, ExtraProductFreeDiscountProvider>();
            services.AddSingleton<IDiscountProvider, AnotherProductPercentageDiscountProvider>();

            services.AddTransient<IBasketCalculationService, BasketCalculationService>();

            services.AddTransient<IDiscountFactory, DiscountFactory>();

            services.AddSingleton<ILogger, FileLogger>();
            services.AddSingleton<IShoppingBasketLogger, ShoppingBasketLogger>();
        }
    }
}