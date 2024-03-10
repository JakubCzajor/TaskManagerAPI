using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAll();
    Task<TaskDto> GetById(int id);
    Task<int> CreateTask(CreateTaskDto dto);
    Task UpdateTask(UpdateTaskDto dto, int id);
    Task DeleteTask(int id);
}