using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, UserRegistration>();
            CreateMap<IdentityRole<Guid>, Role>();

            CreateMap<User, UserInforDto>();
            CreateMap<SaveMessageDto, Message>();
        }
    }
}
