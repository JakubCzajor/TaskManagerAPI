using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string Category { get; set; }
    }
}
