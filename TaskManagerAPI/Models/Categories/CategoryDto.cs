using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}