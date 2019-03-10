namespace ShoppingBasket.Services
{
    public class ItemResult
    {
        public string Id { get; set; }
        public ItemResultAction ItemResultAction { get; set; } 
        // We're using a simple English message here, however an improvement would be to use an error code which can be internationalized or made for accessibility at the POS
        public string Message { get; set; }
    }
}
