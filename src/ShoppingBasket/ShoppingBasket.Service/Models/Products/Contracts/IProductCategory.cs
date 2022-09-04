namespace ShoppingBasket.Service.Models.Products.Contracts
{
    /// <summary>
    /// Product category contract
    /// </summary>
    public interface IProductCategory
    {
        /// <summary>
        /// Gets or sets the abrv.
        /// </summary>
        /// <value>
        /// The abrv.
        /// </value>
        string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
    }
}