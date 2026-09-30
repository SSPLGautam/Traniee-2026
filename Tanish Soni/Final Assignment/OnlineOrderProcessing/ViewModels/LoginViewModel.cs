using System.ComponentModel.DataAnnotations;

namespace OnlineOrderProcessing.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required !")]
        [EmailAddress(ErrorMessage ="Enter a Valid Email !")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
