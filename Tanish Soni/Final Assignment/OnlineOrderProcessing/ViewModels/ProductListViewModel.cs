using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public string? Search {  get; set; }
    }
}
