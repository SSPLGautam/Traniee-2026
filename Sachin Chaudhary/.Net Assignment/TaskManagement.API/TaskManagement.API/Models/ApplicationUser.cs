using Microsoft.AspNetCore.Identity;

namespace TaskManagement.API.Models
{
    public class ApplicationUser: IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
