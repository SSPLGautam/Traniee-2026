using Microsoft.AspNetCore.Identity;

namespace OnlineOrderProcessing.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders {  get; set; } = new List<Order>();

    }
}
