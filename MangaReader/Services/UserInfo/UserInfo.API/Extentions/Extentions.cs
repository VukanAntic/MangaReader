using Microsoft.Extensions.DependencyInjection;

using UserInfo.API.Repository;
using AutoMapper;

namespace UserInfo.API.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection ConfigureReddisDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            return services;
        }

        //public static IServiceCollection Mapper(this IServiceCollection services, IConfiguration configuration)
        //{

        //    services.
        //    //services.AddAutoMapper(configuration =>
        //    //{
        //    //    configuration.CreateMap<UserCreateDTO, User>().ReverseMap();
                
        //    //});

        //    return services;
        //}

        public static IServiceCollection ConfigureMiscellaneousServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInformationRepository, UserInfoReposotory>();

            return services;
        }
    }
}
