using DataAccess.AutoMapper;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Domain.Interfaces;

namespace RealTimeChat.AddServicesCollection
{
    public static class AddTransient
    {
        public static void ConfigureTransient(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureServices (this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
