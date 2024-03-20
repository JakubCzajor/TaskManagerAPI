using TaskManagerAPI.Models.Categories;

namespace TaskManagerAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll();
    Task<CategoryDto> GetById(int id);
    Task<int> CreateCategory(CreateCategoryDto dto);
    Task UpdateCategory(CreateCategoryDto dto, int id);
    Task DeleteCategory(int id);
}