using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDto>> GetAll()
        {
            var tasksDtos = _taskService.GetAll();

            return Ok(tasksDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetById([FromRoute] int id)
        {
            var taskDto = _taskService.GetById(id);

            return Ok(taskDto);
        }

        [HttpPost]
        public ActionResult CreateTask([FromBody] CreateTaskDto dto)
        {
            var id = _taskService.CreateTask(dto);

            return Created($"/api/task/{id}", null);
        }
    }
}
