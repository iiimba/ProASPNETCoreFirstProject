using AutoMapper;
using IISTestApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace IISTestApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUserViewModel, IdentityUser>().ReverseMap();
            CreateMap<UserViewModel, IdentityUser>().ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>().ReverseMap();
            CreateMap<CreateRoleViewModel, IdentityRole>().ReverseMap();
        }
    }
}
