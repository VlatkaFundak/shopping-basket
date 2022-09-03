namespace ShoppingBasket.Service.Infrastructure.Factories
{
    internal static class LoggerFactory
    {
        internal static IShoppingBasketLogger CreateShoppingLogger()
        {
            return new ShoppingBasketLogger();
        }
    }
}