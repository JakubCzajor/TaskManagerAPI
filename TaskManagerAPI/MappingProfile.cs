using AutoMapper;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Models.Tasks;
using TaskManagerAPI.Models.Categories;
using TaskManagerAPI.Models.Accounts;

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

        CreateMap<User, UserProfileDto>();
    }
}