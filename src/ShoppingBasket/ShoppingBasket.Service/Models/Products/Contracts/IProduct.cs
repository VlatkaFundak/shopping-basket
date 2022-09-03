using ShoppingBasket.Service.Models.Discounts.Contracts;

namespace ShoppingBasket.Service.Models.Products.Contracts
{
    public interface IProduct : IBaseModel
    {
        string Name { get; set; }

        decimal Price { get; set; }

        Guid ProductCategoryId { get; set; }

        IEnumerable<IDiscount> Discounts { get; set; }
    }
}