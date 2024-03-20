using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.Categories;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}