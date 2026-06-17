using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Repositories
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetAllWithUsersAsync();
        Task<TaskItem?> GetByIdWithUsersAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByAssignedToAsync(string userId);
    }
}
