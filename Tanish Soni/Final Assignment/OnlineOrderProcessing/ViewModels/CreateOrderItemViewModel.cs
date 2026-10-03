using System.ComponentModel.DataAnnotations;

namespace OnlineOrderProcessing.ViewModels
{
    public class CreateOrderItemViewModel
    {
        [Required (ErrorMessage ="Product is Required !")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is Required !")]
        public int Quantity { get; set; }
    }
}
