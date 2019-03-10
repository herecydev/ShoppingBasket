namespace ShoppingBasket.Services
{
    public static class DecimalExtensions
    {
        public static string RoundTo2DP(this decimal value)
            => string.Format("{0:0.00}", value);
    }
}
