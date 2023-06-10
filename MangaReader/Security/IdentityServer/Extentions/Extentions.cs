using System.Reflection;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.AuthenticationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Extensions;
using Microsoft.AspNetCore.Identity;

using IdentityServer.Data.Interfaces;
using IdentityServer.Data;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Repositories;
using MassTransit;

namespace IdentityServer.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString"),
                    DatabaseName = configuration.GetValue<string>("DatabaseSettings:MongoName")
                },
                IdentityOptionsAction = options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                }
            };

            services.ConfigureMongoDbIdentity<User, Role, Guid>(mongoDbIdentityConfig)
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders();

            return services;
        }
         
        public static IServiceCollection Mapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<UserCreateDTO, User>().ReverseMap();
                configuration.CreateMap<RoleCreateDTO, Role>().ReverseMap();
                configuration.CreateMap<UserCredentialsDTO, User>().ReverseMap();
                configuration.CreateMap<UserDetailsDTO, User>().ReverseMap();
            });

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((_, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
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
            services.AddScoped<IIdentityServerContext, IdentityServerContext>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }
    }
}
