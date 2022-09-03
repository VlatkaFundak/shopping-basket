namespace ShoppingBasket.Service.Common.Filters
{
    public interface IShoppingBasketItemFilterParams : IBaseFilterParams
    {
        string UserIdentifier { get; set; }

        Guid ShoppingBasketId { get; set; }
    }
}