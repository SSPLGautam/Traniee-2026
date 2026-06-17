using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmpTaskManagementMVC.ViewModel.Tasks
{
    public class CreateTaskViewModel
    {
        [Required]

        public string Title { get; set; } = string.Empty;
        [Required]

        public string Description { get; set; } = string.Empty;
        [Required]

        public string AssignedToUserId { get; set; } = string.Empty;
        [Required]

        public DateTime DueDate { get; set; }
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();

    }
}
