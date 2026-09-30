
using System.ComponentModel.DataAnnotations;

namespace OnlineOrderProcessing.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage ="Name is required !!")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Price is required !!")]
        [Range(0.01, 1000000,ErrorMessage ="Price cannot be negative !")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required !!")]
        [Range(1, 10000, ErrorMessage = "Stock cannot be negative !")]
        public int Stock { get; set; }

     

     
    }
}
