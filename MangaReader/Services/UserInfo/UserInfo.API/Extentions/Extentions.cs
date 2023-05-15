using Microsoft.Extensions.DependencyInjection;

using UserInfo.API.Repository;
using AutoMapper;
using UserInfo.API.Entities;
using UserInfo.API.DTOs;

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

        public static IServiceCollection Mapper(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<CreateUserInfoDTO, UserInformation>().ReverseMap();

            });

            return services;
        }

        public static IServiceCollection ConfigureMiscellaneousServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInformationRepository, UserInfoReposotory>();

            return services;
        }
    }
}
