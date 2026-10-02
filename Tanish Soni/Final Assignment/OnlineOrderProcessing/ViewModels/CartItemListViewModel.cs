namespace OnlineOrderProcessing.ViewModels
{
    public class CartItemListViewModel
    {
        public List< CartItemViewModel> CartItems { get; set; }
        public decimal SubTotal { get; set; }

        public int TotalItem { get; set; }
    }
}
