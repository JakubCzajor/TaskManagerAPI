using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models.Categories;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Controllers;

[Route("api/category")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categoryDtos = await _categoryService.GetAll();

        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var categoryDto = await _categoryService.GetById(id);

        return Ok(categoryDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        var id = await _categoryService.CreateCategory(dto);

        return Created($"/api/task/{id}", null);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> UpdateCategory([FromBody] CreateCategoryDto dto, int id)
    {
        await _categoryService.UpdateCategory(dto, id);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> DeleteCategory([FromRoute] int id)
    {
        await _categoryService.DeleteCategory(id);

        return NoContent();
    }
}