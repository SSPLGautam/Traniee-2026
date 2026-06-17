using TaskManagement.API.DTOs;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.UnitOfWork.UnitOfWork;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskResponseDto>> GetAllAsync()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();

            return tasks.Select(x => new TaskResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AssignedToUserId = x.AssignedToUserId,
                AssignedByUserId = x.AssignedByUserId,
                Status = x.Status,
                DueDate = x.DueDate,
                CreatedDate = x.CreatedDate
            }).ToList();
        }

        public async Task<List<TaskResponseDto>> GetMyTasksAsync(string userId)
        {
            var tasks = await _unitOfWork.Tasks.FindAsync1(x =>
                x.AssignedToUserId == userId);

            return tasks.Select(x => new TaskResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AssignedToUserId = x.AssignedToUserId,
                AssignedByUserId = x.AssignedByUserId,
                Status = x.Status,
                DueDate = x.DueDate,
                CreatedDate = x.CreatedDate
            }).ToList();
        }

        public async Task<TaskResponseDto?> GetByIdAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null)
                return null;

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedToUserId = task.AssignedToUserId,
                AssignedByUserId = task.AssignedByUserId,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedDate = task.CreatedDate
            };
        }

        public async Task<TaskResponseDto> CreateAsync(
            CreateTaskDto dto,
            string assignedByUserId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                AssignedToUserId = dto.AssignedToUserId,
                AssignedByUserId = assignedByUserId,
                Status = "Pending",
                DueDate = dto.DueDate,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedToUserId = task.AssignedToUserId,
                AssignedByUserId = task.AssignedByUserId,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedDate = task.CreatedDate
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateTaskDto dto)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null)
                return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            task.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            if (task == null)
                return false;

            _unitOfWork.Tasks.Delete(task);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}