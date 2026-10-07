namespace OnlineOrderProcessing.ViewModels
{
    public class AdminOrdersViewModel
    {
        public List<AdminOrderViewModel> Orders { get; set; } = new();

        public List<OnlineOrderProcessing.Enums.OrderStatus> OrderStatuses { get; set; }
            = new();
    }
}