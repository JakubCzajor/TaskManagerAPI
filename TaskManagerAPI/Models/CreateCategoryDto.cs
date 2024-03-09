using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}