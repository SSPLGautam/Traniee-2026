using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories;
using TaskManagement.API.Repositories.Taskrepo;

public class TaskRepository: GenericRepository<TaskItem>, ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId)
    {
        return await _context.Tasks
            .Where(t => t.AssignedToUserId == userId)
            .ToListAsync();
    }
}