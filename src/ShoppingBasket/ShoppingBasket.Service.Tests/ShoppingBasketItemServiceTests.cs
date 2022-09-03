using Moq;
using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Services.Shopping;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    public class ShoppingBasketItemServiceTests
    {
        [Fact]
        public async void AddShoppingBasketItemAsync_ShouldAddToArray()
        {
            //Arange
            var products = TestData.CreateProducts();
            var shoppingBasketItemService = new ShoppingBasketItemService();
            var shoppingBasketService = new ShoppingBasketService(Mock.Of<IBasketCalculationService>());

            var basket = await shoppingBasketService.CreateShoppingBasketAsync("user_cookie_1");

            var items = await shoppingBasketItemService.FindShoppingBasketItemAsync(new ShoppingBasketItemFilterParams { UserIdentifier = basket.UserIdentifier });
            var expected = items.Count() + products.Count();
            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            foreach (var product in products)
            {
                var item = await shoppingBasketItemService.CreateShoppingBasketItemAsync(basket.Id);
                item.ProductId = product.Id;
                item.Product = product;
                item.Quantity = 5;

                newShoppingBasketItems.Add(item);
            }

            //Act
            await shoppingBasketItemService.AddShoppingBasketItemAsync(newShoppingBasketItems);
            items = await shoppingBasketItemService.FindShoppingBasketItemAsync(new ShoppingBasketItemFilterParams { UserIdentifier = basket.UserIdentifier });

            //Assert
            Assert.Equal(expected, items.Count());
        }
    }
}