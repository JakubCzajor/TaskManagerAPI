using TaskManagerAPI.Entities;

namespace TaskManagerAPI
{
    public class Seeder
    {
        private readonly TaskManagerDbContext _context;

        public Seeder(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                var categories = GetCategories();
                if (!_context.Categories.Any())
                {
                    _context.Categories.AddRange(categories);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Software Development"
                },
                new Category()
                {
                    Name = "Network Administration"
                },
                new Category()
                {
                    Name = "Quality Assurance"
                },
                new Category()
                {
                    Name = "Project Management"
                }
            };

            return categories;
        }
    }
}
