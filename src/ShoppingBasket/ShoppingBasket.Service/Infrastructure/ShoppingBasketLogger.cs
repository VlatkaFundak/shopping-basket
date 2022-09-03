using Newtonsoft.Json;
using ShoppingBasket.Service.Models;
using System.Reflection;

namespace ShoppingBasket.Service.Infrastructure
{
    internal class ShoppingBasketLogger : FileLogger, IShoppingBasketLogger //log4net usually
    {
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