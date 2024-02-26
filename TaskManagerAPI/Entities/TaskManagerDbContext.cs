using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Entities
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Task>()
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Task>()
                .Property(t => t.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Task>()
                .Property(t => t.LastModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<Task>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId);


            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();
        }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
            
        }
    }
}
