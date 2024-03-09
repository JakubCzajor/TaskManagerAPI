using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models;

public class UpdateTaskDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }
}