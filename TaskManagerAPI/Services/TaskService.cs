using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Authorization;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services;

public class TaskService : ITaskService
{
    private readonly TaskManagerDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<TaskService> _logger;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public TaskService(TaskManagerDbContext context, IMapper mapper, ILogger<TaskService> logger,
        IUserContextService userContextService, IAuthorizationService authorizationService)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
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
        await FindCategoryById(dto.CategoryId);
        var task = _mapper.Map<CustomTask>(dto);
        task.CreatedById = _userContextService.GetUserId;
        _context.Tasks.Add(task);
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

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, task,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded && !_userContextService.User.IsInRole("Admin"))
            throw new ForbidException();

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

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, task,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded && !_userContextService.User.IsInRole("Admin"))
            throw new ForbidException();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    private async Task FindCategoryById(int categoryId)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new NotFoundException("Category not found.");
    }
}