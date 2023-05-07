using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var mongoDbName = configuration.GetValue<string>("DatabaseSettings:MongoName");
            services.AddIdentity<User, Role>()
                .AddMongoDbStores<User, Role, Guid>(
                mongoDbSettings, mongoDbName
                );

            return services;
        }

        public static IServiceCollection Mapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<UserCreateDTO, User>().ReverseMap();
            });


            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<RoleCreateDTO, Role>().ReverseMap();
            });

            return services;
        }


    }
}
