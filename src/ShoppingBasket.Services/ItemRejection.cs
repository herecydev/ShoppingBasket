namespace ShoppingBasket.Services
{
    public class ItemRejection
    {
        public string Id { get; set; }
        // We're using a simple English message here, however an improvement would be to use an error code which can be internationalized or made for accessibility at the POS
        public string Message { get; set; }
    }
}
