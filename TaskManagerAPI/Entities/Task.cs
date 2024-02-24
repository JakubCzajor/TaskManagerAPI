using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set;} = DateTime.Now;

        public int CategoryId {  get; set; }
        public virtual Category Category { get; set; }
    }
}
