using TaskManagerAPI.Models.Tasks;

namespace TaskManagerAPI.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAll();
    Task<IEnumerable<TaskDto>> GetDoneTasks();
    Task<IEnumerable<TaskDto>> GetActiveTasks();
    Task<TaskDto> GetById(int id);
    Task<int> CreateTask(CreateTaskDto dto);
    Task UpdateTask(UpdateTaskDto dto, int id);
    Task DeleteTask(int id);
    Task SetTaskAsDone(int id);
}