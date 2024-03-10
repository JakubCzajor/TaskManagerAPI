using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services.Interfaces;

public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAll();
    CategoryDto GetById(int id);
    int CreateCategory(CreateCategoryDto dto);
    void UpdateCategory(CreateCategoryDto dto, int id);
    void DeleteCategory(int id);
}