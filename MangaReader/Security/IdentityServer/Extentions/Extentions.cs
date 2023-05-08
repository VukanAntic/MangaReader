using System.Reflection;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.AuthenticationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetSection("secretKey").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureMiscellaneousServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
