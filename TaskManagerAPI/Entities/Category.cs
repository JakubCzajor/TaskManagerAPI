namespace TaskManagerAPI.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<CustomTask> Tasks { get; set; }
}