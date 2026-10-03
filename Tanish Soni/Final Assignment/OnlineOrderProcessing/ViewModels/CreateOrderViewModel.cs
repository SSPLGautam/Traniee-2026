using System.ComponentModel.DataAnnotations;

namespace OnlineOrderProcessing.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage ="Key Missing !")]
        public string OrderRequestKey { get; set; } = string.Empty;

        [Required(ErrorMessage ="Items required to make an order ")]
        public List<CreateOrderItemViewModel> Items { get; set; }

    }

}
