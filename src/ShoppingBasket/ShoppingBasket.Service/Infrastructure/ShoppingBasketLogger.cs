using Newtonsoft.Json;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using System.Reflection;

namespace ShoppingBasket.Service.Infrastructure
{
    /// <summary>
    /// Shopping basket logger.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.FileLogger" />
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.IShoppingBasketLogger" />
    internal class ShoppingBasketLogger : FileLogger, IShoppingBasketLogger //log4net usually
    {
        /// <summary>
        /// Logs the total asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        public async Task LogTotalAsync(IShoppingBasket shoppingBasket)
        {
            var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (StreamWriter writer = File.AppendText(filePath + "\\" + "log.txt"))
            {
                await writer.WriteAsync($"Total calculated at {DateTime.Now} : \n");
                await writer.WriteAsync($"\t\t Basket owner: {shoppingBasket.UserIdentifier} \n");
                await writer.WriteAsync($"\t\t Items: {JsonConvert.SerializeObject(shoppingBasket.ShoppingBasketItems)} \n");
                await writer.WriteAsync($"\t\t Discounts: {JsonConvert.SerializeObject(shoppingBasket.Discounts)}  \n");
                await writer.WriteAsync($"\t\t Total: {shoppingBasket.Total}  \n");
            }
        }
    }
}