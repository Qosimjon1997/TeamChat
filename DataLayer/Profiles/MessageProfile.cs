using AutoMapper;
using DataLayer.Dtos.MessageDtos;
using DataLayer.Models;

namespace DataLayer.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadDto>();
            CreateMap<MessageCreateDto, Message>();
        }
    }
}
