using AutoMapper;
using DataLayer.Dtos.ApplicationUserDtos;
using DataLayer.Models.Auth;

namespace DataLayer.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserReadDto>();
        }
    }
}
