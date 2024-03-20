using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Models.Tasks;

public class CreateTaskDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    public int CategoryId { get; set; }
}