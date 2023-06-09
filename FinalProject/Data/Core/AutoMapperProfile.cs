using AutoMapper;
using FinalProject.Data.Entities;
using FinalProject.Data.Models.DTOs;

namespace FinalProject.Data.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
        }
        
    }
}
