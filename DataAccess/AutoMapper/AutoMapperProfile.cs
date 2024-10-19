using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;

namespace DataAccess.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, UserRegistration>();
        }
    }
}
