using TaskManagerAPI.Entities;

namespace TaskManagerAPI;

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
            if (!_context.Roles.Any())
            {
                var roles = GetRoles();
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }

            if (!_context.Categories.Any())
            {
                var categories = GetCategories();
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

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>()
        {
            new Role()
            {
                Name = "User"
            },
            new Role()
            {
                Name = "Manager"
            },
            new Role()
            {
                Name = "Admin"
            }
        };

        return roles;
    }
}