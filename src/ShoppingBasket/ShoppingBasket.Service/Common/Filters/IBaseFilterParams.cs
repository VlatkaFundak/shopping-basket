namespace ShoppingBasket.Service.Common.Filters
{
    public interface IBaseFilterParams
    {
        IEnumerable<Guid> ids { get; set; }
    }
}