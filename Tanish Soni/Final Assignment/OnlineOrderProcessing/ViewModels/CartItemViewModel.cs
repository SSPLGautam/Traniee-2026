using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; } 

        public Product Product { get; set; } = null!;

        public decimal Price { get; set; }



    }
}
