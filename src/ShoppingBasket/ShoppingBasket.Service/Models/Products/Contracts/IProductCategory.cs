namespace ShoppingBasket.Service.Models.Products.Contracts
{
    public interface IProductCategory
    {
        string Abrv { get; set; }
        string Description { get; set; }
        string Name { get; set; }
    }
}