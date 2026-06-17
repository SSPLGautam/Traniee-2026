using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The first name field is required.")]
        public  required string FirstName { get; set; }
        [Required(ErrorMessage = "The last name field is required.")]   
        public required string LastName { get; set; }
        [Required(ErrorMessage = "The email field is required.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "The password field is required.")]
        public required string Password { get; set; }   
    

    

    }
}
