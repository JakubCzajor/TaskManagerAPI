using AutoMapper;
using TaskManagerAPI.Models;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Task
            CreateMap<Entities.Task, TaskDto>()
                .ForMember(t => t.Category, c => c.MapFrom(s => s.Category.Name));

            CreateMap<CreateTaskDto, Entities.Task>();

            // Category
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
