namespace OnlineOrderProcessing.ViewModels
{
    public class SalesReportViewModel
    {
        public int TotalOrders { get; set; }
        public int SuccessfulOrders { get; set; }
        public int CancelledOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<TopProductViewModel> TopProducts { get; set; } = new();
    }

    public class TopProductViewModel
    {
        public string ProductName { get; set; } = "";
        public int QuantitySold { get; set; }
    }
}