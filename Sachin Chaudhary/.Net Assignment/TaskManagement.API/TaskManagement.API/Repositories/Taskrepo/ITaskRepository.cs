using TaskManagement.API.Models;
using TaskManagement.API.Repositories.GenericRepository;

namespace TaskManagement.API.Repositories.Taskrepo
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId);

    }
}
