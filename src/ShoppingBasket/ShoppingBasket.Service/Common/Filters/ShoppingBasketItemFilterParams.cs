namespace ShoppingBasket.Service.Common.Filters
{
    public class ShoppingBasketItemFilterParams : BaseFilterParams, IShoppingBasketItemFilterParams
    {
        public string UserIdentifier { get; set; }

        public Guid ShoppingBasketId { get; set; }
    }
}