using ShoppingBasket.Service.Models.Discounts.Contracts;
using ShoppingBasket.Service.Models.Discounts.Enums;

namespace ShoppingBasket.Service.Providers
{
    public class AnotherProductPercentageDiscountProvider : IDiscountProvider
    {
        public DiscountType DiscountType => DiscountType.ProductPercentage;

        public Task<decimal> GetDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IDiscount> discounts)
        {
            return CalculateDiscountAsync(shoppingBasketItems, discounts.Cast<IAnotherProductPercentageDiscount>());
        }

        protected internal Task<decimal> CalculateDiscountAsync(IEnumerable<IShoppingBasketItem> shoppingBasketItems, IEnumerable<IAnotherProductPercentageDiscount> discounts)
        {
            decimal discountResult = 0m;

            foreach (var discount in discounts)
            {
                var mainProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.MainProductId);
                if (mainProduct is not null && mainProduct.Quantity >= discount.MainQuantity)
                {
                    var discountProduct = shoppingBasketItems.FirstOrDefault(p => p.ProductId == discount.DiscountProductId);
                    if (discountProduct is null)
                    {
                        return Task.FromResult(0m);
                    }

                    var quantityDiscount = (int)Math.Floor(mainProduct.Quantity / discount.MainQuantity);

                    if (discountProduct.Quantity <= quantityDiscount)
                    {
                        discountResult += discountProduct.Quantity * ((discount.Percentage / 100m) * discountProduct.Product.Price);
                    }

                    if (discountProduct.Quantity > quantityDiscount)
                    {
                        discountResult += quantityDiscount * (discount.Percentage / 100m) * discountProduct.Product.Price;
                    }
                }
            }

            return Task.FromResult(discountResult);
        }
    }
}