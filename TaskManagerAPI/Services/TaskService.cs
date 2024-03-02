using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll();
        TaskDto GetById(int id);
        int CreateTask(CreateTaskDto dto);
        void UpdateTask(UpdateTaskDto dto, int id);
    }

    public class TaskService : ITaskService
    {
        private readonly TaskManagerDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(TaskManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TaskDto> GetAll()
        {
            var tasks = _context
                .Tasks
                .Include(t => t.Category)
                .ToList();

            var tasksDtos = _mapper.Map<List<TaskDto>>(tasks);

            return tasksDtos;
        }

        public TaskDto GetById(int id)
        {
            var task = _context
                .Tasks
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);

            if (task is null)
                throw new NotFoundException("Task not found");

            var result = _mapper.Map<TaskDto>(task);

            return result;
        }

        public int CreateTask(CreateTaskDto dto)
        {
            findCategoryById(dto.CategoryId);
            var task = _mapper.Map<Entities.Task>(dto);
            _context.Add(task);
            _context.SaveChanges();

            return task.Id;
        }

        public void UpdateTask(UpdateTaskDto dto, int id)
        {
            var task = _context
                .Tasks
                .FirstOrDefault(t => t.Id == id);

            if (task is null)
                throw new NotFoundException("Task not found");

            task.Name = dto.Name;
            task.Description = dto.Description;
            task.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();
        }

        private void findCategoryById(int categoryId)
        {
            var category = _context
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (category is null)
                throw new NotFoundException("Category not found");
        }
    }
}
