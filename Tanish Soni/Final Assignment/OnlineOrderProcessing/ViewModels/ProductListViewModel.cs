using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public string? Search {  get; set; }
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
