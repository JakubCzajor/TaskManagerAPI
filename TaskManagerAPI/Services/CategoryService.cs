using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly TaskManagerDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(TaskManagerDbContext context, IMapper mapper, ILogger<CategoryService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var categories = await _context
            .Categories
            .ToListAsync();

        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new NotFoundException("Category not found.");

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<int> CreateCategory(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        var categoryAlreadyExists = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);

        if (categoryAlreadyExists is not null)
            throw new BadRequestException($"Category already exists.");

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category.Id;
    }

    public async Task UpdateCategory(CreateCategoryDto dto, int id)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new NotFoundException("Category not found.");

        category.Name = dto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(int id)
    {
        _logger.LogError($"Category with id: {id} DELETE action called");

        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new NotFoundException("Category not found.");

        var tasksCount = await _context.Tasks.CountAsync(t => t.CategoryId == id);

        if (tasksCount > 0)
            throw new ConflictException("Cannot delete category because it has associated tasks.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}