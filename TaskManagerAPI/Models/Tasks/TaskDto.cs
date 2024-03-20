using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Models.Tasks;

public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public string Category { get; set; }
}