using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagementAPI.Dto.TasksDto
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]

        public string Description { get; set; } = string.Empty;
        [Required]

        public string AssignedToUserId { get; set; } = string.Empty;



        public DateTime DueDate { get; set; }
    }
}
