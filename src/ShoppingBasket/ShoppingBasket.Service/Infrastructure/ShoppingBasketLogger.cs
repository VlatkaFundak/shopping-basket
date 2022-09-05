using Newtonsoft.Json;
using ShoppingBasket.Service.Common.Enums;
using ShoppingBasket.Service.Models.ShoppingBasketDetails.Contracts;
using System.Reflection;
using System.Text;

namespace ShoppingBasket.Service.Infrastructure
{
    /// <summary>
    /// Shopping basket logger.
    /// </summary>
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.FileLogger" />
    /// <seealso cref="ShoppingBasket.Service.Infrastructure.IShoppingBasketLogger" />
    internal class ShoppingBasketLogger : IShoppingBasketLogger //log4net usually
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns></returns>
        private static string GetPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Logs the total asynchronous.
        /// </summary>
        /// <param name="shoppingBasket">The shopping basket.</param>
        /// <returns></returns>
        public Task LogTotalAsync(IShoppingBasket shoppingBasket)
        {
            StringBuilder sb = new();

            sb.AppendLine($"Total calculated at {DateTime.Now} :");
            sb.AppendLine($"\t Basket owner: {shoppingBasket.UserIdentifier}");
            sb.AppendLine("\t Product details:");
            foreach (var item in shoppingBasket.ShoppingBasketItems)
            {
                sb.AppendLine($"\t\t Name: {item.Product.Name}");
                sb.AppendLine($"\t\t Unit price: {item.Product.Price}");
                sb.AppendLine($"\t\t Quantity: {item.Quantity}");
                sb.AppendLine($"\t\t Total price: {item.Product.Price * (decimal)item.Quantity}");
                sb.AppendLine($"\t\t Discount: {decimal.Round(item.DiscountAmount, 2)}");
                sb.AppendLine($"\t\t Discount type: {item.Discount?.DiscountType ?? DiscountType.None}");
                sb.AppendLine();
            }

            sb.AppendLine($"\t Total: {decimal.Round(shoppingBasket.Total, 2)}");
            sb.AppendLine("----------------------------------------------------");

            return LogMessage(sb.ToString());
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public Task LogErrorAsync(string message, Exception ex)
        {
            return LogMessage($"{message}, Exception: {JsonConvert.SerializeObject(ex)}");
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task LogErrorAsync(string message)
        {
            return LogMessage($"Error message: {message}");
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task LogInfoAsync(string message)
        {
            return LogMessage($"Info message: {message}");
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static Task LogMessage(string message)
        {
            using StreamWriter writer = File.AppendText(GetPath() + "\\" + "log.txt");
            return writer.WriteAsync(message);
        }
    }
}