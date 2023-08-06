using AutoMapper;
using TaskPlanner.DTOs;
using TaskPlanner.Models;

namespace TaskPlanner.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
