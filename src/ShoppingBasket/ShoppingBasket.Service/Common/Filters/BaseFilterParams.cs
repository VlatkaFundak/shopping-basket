namespace ShoppingBasket.Service.Common.Filters
{
    public class BaseFilterParams : IBaseFilterParams
    {
        public IEnumerable<Guid> ids { get; set; }
    }
}