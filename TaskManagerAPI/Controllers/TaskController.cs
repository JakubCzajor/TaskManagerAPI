using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers;

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
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
    {
        var tasksDtos = await _taskService.GetAll();

        return Ok(tasksDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetById([FromRoute] int id)
    {
        var taskDto = await _taskService.GetById(id);

        return Ok(taskDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var id = await _taskService.CreateTask(dto);

        return Created($"/api/task/{id}", null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask([FromBody] UpdateTaskDto dto, [FromRoute] int id)
    {
        await _taskService.UpdateTask(dto, id);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task <ActionResult> DeleteTask([FromRoute] int id)
    {
        await _taskService.DeleteTask(id);

        return NoContent();
    }
}