namespace ShoppingBasket.Service.Models.Discounts.Factories
{
    public static class DiscountFacade
    {
        public static IDiscountFactory CreateDiscountFactory()
        {
            return new DiscountFactory();
        }
    }
}