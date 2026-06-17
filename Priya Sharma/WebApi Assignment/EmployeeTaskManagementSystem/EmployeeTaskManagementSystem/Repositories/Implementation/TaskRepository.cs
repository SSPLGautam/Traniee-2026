using Microsoft.EntityFrameworkCore;
using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.Models;
using TaskManagementSystemApi.Repositories.unitOfWork;

namespace TaskManagementSystemApi.Repositories.Implementation
{
    public class TaskRepository : GenericRepository<TaskItem> , ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<TaskItem>> GetAllWithUsersAsync()
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.AssignedBy)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdWithUsersAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.AssignedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByAssignedToAsync(string userId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.AssignedBy)
                .Where(t => t.AssignedToUserId == userId)
                .ToListAsync();
        }
    }
}
