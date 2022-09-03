namespace ShoppingBasket.Service.Models.Factories
{
    internal static class ShoppingBasketFactory
    {
        internal static IShoppingBasket CreateShoppingBasket()
        {
            return new ShoppingBasket();
        }
    }
}