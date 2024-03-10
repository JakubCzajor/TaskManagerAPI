using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAll();
    Task<TaskDto> GetById(int id);
    Task<int> CreateTask(CreateTaskDto dto);
    Task UpdateTask(UpdateTaskDto dto, int id);
    Task DeleteTask(int id);
}

public class TaskService : ITaskService
{
    private readonly TaskManagerDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<TaskService> _logger;

    public TaskService(TaskManagerDbContext context, IMapper mapper, ILogger<TaskService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskDto>> GetAll()
    {
        var tasks = await _context
            .Tasks
            .Include(t => t.Category)
            .ToListAsync();

        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetById(int id)
    {
        var task = await _context
            .Tasks
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            throw new NotFoundException("Task not found.");

        return _mapper.Map<TaskDto>(task);
    }

    public async Task<int> CreateTask(CreateTaskDto dto)
    {
        await findCategoryById(dto.CategoryId);
        var task = _mapper.Map<Entities.CustomTask>(dto);
        _context.Add(task);
        await _context.SaveChangesAsync();

        return task.Id;
    }

    public async Task UpdateTask(UpdateTaskDto dto, int id)
    {
        var task = await _context
            .Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            throw new NotFoundException("Task not found.");

        task.Name = dto.Name;
        task.Description = dto.Description;
        task.LastModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(int id)
    {
        _logger.LogError($"Task with id: {id} DELETE action called");

        var task = await _context
            .Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            throw new NotFoundException("Task not found.");

        _context.Remove(task);
        await _context.SaveChangesAsync();
    }

    private async Task findCategoryById(int categoryId)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new NotFoundException("Category not found.");
    }
}