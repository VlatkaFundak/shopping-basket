namespace ShoppingBasket.Infrastructure.Service.Infrastructure.Extensions
{
    public struct GuidExtensions
    {
        public static bool IsNullOrEmpty(Guid? value)
        {
            if (Guid.Equals(value, null) || Guid.Equals(value, Guid.Empty))
            {
                return false;
            }

            return true;
        }
    }
}