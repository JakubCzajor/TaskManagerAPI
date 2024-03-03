﻿using AutoMapper;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Exceptions;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        int CreateCategory(CreateCategoryDto dto);
        void UpdateCategory(CreateCategoryDto dto, int id);
        void DeleteCategory(int id);
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

        public int CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            var categoryAlreadyExists = _context.Categories.FirstOrDefault(c => c.Name == category.Name);

            if (categoryAlreadyExists is not null)
                throw new BadRequestException($"Category already exists.");

            _context.Add(category);
            _context.SaveChanges();

            return category.Id;
        }

        public void UpdateCategory(CreateCategoryDto dto, int id)
        {
            var category = _context
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException("Category not found.");

            category.Name = dto.Name;
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException("Category not found.");

            _context.Remove(category);
            _context.SaveChanges();
        }
    }
}
