using Moq;
using ShoppingBasket.Service.Common.Filters;
using ShoppingBasket.Service.Models;
using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Services.Shopping;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ShoppingBasket.Service.Tests
{
    [ExcludeFromCodeCoverage]
    public class ShoppingBasketItemServiceTests
    {
        private Fixture fixture;

        public ShoppingBasketItemServiceTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public async void AddShoppingBasketItemAsync_ShouldAddNewItems()
        {
            //Arange
            var products = TestData.CreateProducts();
            var shoppingBasketItemService = new ShoppingBasketItemService();

            var basket = await fixture.shoppingBasketService.Object.CreateShoppingBasketAsync("user_cookie_info_1");

            var items = await shoppingBasketItemService.FindShoppingBasketItemAsync(fixture.shoppingBasketItemFilterParams.Object, null, null);

            var expected = items.Count() + products.Count();
            var newShoppingBasketItems = new List<IShoppingBasketItem>();

            await Task.WhenAll(products.Select(async (product) =>
            {
                var item = await shoppingBasketItemService.CreateShoppingBasketItemAsync(basket.Id);
                item.ProductId = product.Id;
                item.Product = product;
                item.Quantity = 5;

                newShoppingBasketItems.Add(item);
            }).ToArray());

            //Act
            await shoppingBasketItemService.AddShoppingBasketItemAsync(newShoppingBasketItems);
            items = await shoppingBasketItemService.FindShoppingBasketItemAsync(fixture.shoppingBasketItemFilterParams.Object, null, null);

            //Assert
            Assert.Equal(expected, items.Count());
        }

        private class Fixture
        {
            public Mock<IBasketCalculationService> basketCalculationService;
            public Mock<IShoppingBasketService> shoppingBasketService;
            public Mock<IShoppingBasketItemFilterParams> shoppingBasketItemFilterParams;

            public Fixture()
            {
                basketCalculationService = new Mock<IBasketCalculationService>();
                shoppingBasketService = new Mock<IShoppingBasketService>();
                shoppingBasketItemFilterParams = new Mock<IShoppingBasketItemFilterParams>();

                IShoppingBasket shoppingBasket = new ShoppingBasketMock();
                shoppingBasket.Id = Guid.NewGuid();
                shoppingBasket.UserIdentifier = "user_cookie_info_1";
                shoppingBasket.DateCreated = DateTime.Now;
                shoppingBasket.DateUpdated = DateTime.Now;

                shoppingBasketItemFilterParams.Setup(p => p.ShoppingBasketId).Returns(shoppingBasket.Id);

                shoppingBasketService.Setup(p => p.CreateShoppingBasketAsync(shoppingBasket.UserIdentifier))
                    .Returns(Task.FromResult(shoppingBasket));
            }
        }

        private class ShoppingBasketMock : IShoppingBasket
        {
            public string UserIdentifier { get; set; }
            public IEnumerable<IDiscount> Discounts { get; set; }
            public IEnumerable<IShoppingBasketItem> ShoppingBasketItems { get; set; }
            public decimal Total { get; set; }
            public Guid Id { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
        }
    }
}