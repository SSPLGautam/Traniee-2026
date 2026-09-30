namespace OnlineOrderProcessing.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string SKU { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
