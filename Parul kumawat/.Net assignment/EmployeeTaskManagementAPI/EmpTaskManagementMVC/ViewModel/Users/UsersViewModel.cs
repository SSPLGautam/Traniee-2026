using System.ComponentModel.DataAnnotations;

namespace EmpTaskManagementMVC.ViewModel.Users
{
    public class UsersViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required]

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        [Required]

        public string UserName { get; set; }= string.Empty;
        [Required]

        public string Email { get; set; } = string.Empty;
    }
}
