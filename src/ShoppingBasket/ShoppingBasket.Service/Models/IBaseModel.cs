namespace ShoppingBasket.Service.Models
{
    public interface IBaseModel
    {
        Guid Id { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateUpdated { get; set; }
    }
}