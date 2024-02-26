using AutoMapper;
using TaskManagerAPI.Models;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Task, TaskDto>()
                .ForMember(t => t.Category, c => c.MapFrom(s => s.Category.Name));

            CreateMap<CreateTaskDto, Entities.Task>();
        }
    }
}
