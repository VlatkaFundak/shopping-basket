namespace ShoppingBasket.Infrastructure.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Guid extensions.
    /// </summary>
    public struct GuidExtensions
    {
        /// <summary>
        /// Determines whether [is null or empty] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(Guid? value)
        {
            return Guid.Equals(value, null) || Guid.Equals(value, Guid.Empty) ? true : false;
        }
    }
}