using ArtCircler.Controllers.Api;
using ArtCircler.Dtos;
using ArtCircler.Models;
using AutoMapper;

namespace ArtCircler.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Event, EventDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
    }
}