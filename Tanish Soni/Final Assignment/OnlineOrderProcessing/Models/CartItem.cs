namespace OnlineOrderProcessing.Models
{
    public class CartItem
    {
        public string UserId { get; set; }
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

       

             
    }
}
