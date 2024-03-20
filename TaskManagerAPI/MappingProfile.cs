using AutoMapper;
using TaskManagerAPI.Models;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Task
        CreateMap<CustomTask, TaskDto>()
            .ForMember(t => t.Category, c => c.MapFrom(s => s.Category.Name));

        CreateMap<CreateTaskDto, CustomTask>();

        // Category
        CreateMap<Category, CategoryDto>();

        CreateMap<CreateCategoryDto, Category>();

        // User
        CreateMap<User, UserDto>()
            .ForMember(u => u.Role, r => r.MapFrom(s => s.Role.Name));
    }
}