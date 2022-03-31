using AutoMapper;
using DataLayer.Dtos.NotificationDtos;
using DataLayer.Models;

namespace DataLayer.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto,  Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
            CreateMap<Notification, NotificationUpdateDto>();
        }
    }
}
