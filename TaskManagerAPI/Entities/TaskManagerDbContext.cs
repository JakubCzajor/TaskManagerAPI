using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Entities
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Task
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
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // User
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            // Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();
        }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {

        }
    }
}
