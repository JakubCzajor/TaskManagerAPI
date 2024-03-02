using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models
{
    public class CategoryDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
