using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll();
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
    }
}
