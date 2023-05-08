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
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
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
