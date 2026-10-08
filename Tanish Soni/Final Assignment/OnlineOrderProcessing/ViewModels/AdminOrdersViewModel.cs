namespace OnlineOrderProcessing.ViewModels
{
    public class AdminOrdersViewModel
    {
        public List<AdminOrderViewModel> Orders { get; set; } = new();

        public List<OnlineOrderProcessing.Enums.OrderStatus> OrderStatuses { get; set; }
            = new();
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}