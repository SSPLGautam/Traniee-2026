using System.ComponentModel.DataAnnotations;

namespace OnlineOrderProcessing.Models
{
    public class Product
    {
       public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string SKU { get; set; } = null!;
        public decimal Price { get; set; }

        public int Stock { get; set; }  
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
