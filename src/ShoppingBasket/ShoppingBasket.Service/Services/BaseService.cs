using ShoppingBasket.Infrastructure.Service.Infrastructure.Extensions;
using ShoppingBasket.Service.Models;

namespace ShoppingBasket.Service.Services
{
    /// <summary>
    /// Base service
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected virtual Task InitializeAsync<T>(T item) where T : IBaseModel
        {
            if (GuidExtensions.IsNullOrEmpty(item.Id))
            {
                item.Id = Guid.NewGuid();
            }

            if (item.DateCreated.Equals(DateTime.MinValue))
            {
                item.DateCreated = DateTime.UtcNow;
            }

            item.DateUpdated = DateTime.UtcNow;

            return Task.CompletedTask;
        }
    }
}