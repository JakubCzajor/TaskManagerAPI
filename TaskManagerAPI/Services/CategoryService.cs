using AutoMapper;
using System.Threading.Tasks;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        int CreateCategory(CategoryDto dto);
    }

    public class CategoryService : ICategoryService
    {
        private readonly TaskManagerDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(TaskManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _context
                .Categories
                .ToList();

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);

            return categoriesDtos;
        }

        public CategoryDto GetById(int id)
        {
            var category = _context
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException("Category not found.");

            var result = _mapper.Map<CategoryDto>(category);

            return result;
        }

        public int CreateCategory(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category.Id;
        }
    }
}
