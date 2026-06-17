namespace TaskManagement.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public required string AssignedToUserId { get; set; }

        public required string AssignedByUserId { get; set; }

        public required string Status { get; set; } 
        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }


    }
}
