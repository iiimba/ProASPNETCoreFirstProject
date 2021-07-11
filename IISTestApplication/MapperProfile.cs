using AutoMapper;
using IISTestApplication.Models;
using IISTestApplication.Models.MapperModels;
using Microsoft.AspNetCore.Identity;

namespace IISTestApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUserViewModel, IdentityUser>(MemberList.None);
            CreateMap<UserViewModel, IdentityUser>(MemberList.None).ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>(MemberList.None).ReverseMap();
            CreateMap<CreateRoleViewModel, IdentityRole>(MemberList.None);

            CreateMap<ModelWithShort, ModelWithInt>().ReverseMap();
            CreateMap<ModelWithInt, ModelWithDouble>().ReverseMap();
            CreateMap<ModelWithNullable, ModelWithoutNullable>()
                .ForMember(dest => dest.AdditionalField2, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CalendarEvent, CalendarEventForm>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(s => s.Date))
                .ForMember(dest => dest.EventHour, opt => opt.MapFrom(s => s.Date.Hour))
                .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(s => s.Date.Minute));

            CreateMap<Car, CarDTO>();
            CreateMap<Engine, EngineDTO>();
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile<MapperProfile>();
            });
        }
    }
}
