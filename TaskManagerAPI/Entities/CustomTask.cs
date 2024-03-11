using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Entities;

public class CustomTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    public bool? IsDone { get; set; } = false;

    public int? CreatedById { get; set; }
    public virtual User CreatedBy { get; set; }

    public int CategoryId {  get; set; }
    public virtual Category Category { get; set; }
}