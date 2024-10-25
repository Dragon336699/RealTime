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
            CreateMap<IdentityRole<int>, Role>();
        }
    }
}
