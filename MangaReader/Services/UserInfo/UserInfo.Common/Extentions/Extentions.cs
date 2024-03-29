﻿using Microsoft.Extensions.DependencyInjection;
using UserInfo.Common.Repository;
using AutoMapper;
using UserInfo.Common.Entities;
using UserInfo.Common.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;
using EventBus.Messages.Events;
using UserInfo.Common.EventBusConsumers;
using EventBus.Messages.Constants;

namespace UserInfo.Common.Extentions
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
               
                configuration.CreateMap<CreateUserInfoDTO, UserIsCreatedEvent>().ReverseMap();
                configuration.CreateMap<UpdateUserInfoDTO, UpdateAllReadMangaEvent>().ReverseMap();

            });

            services.AddMassTransit(config =>
            {
                config.AddConsumer<UserIsCreatedConsumer>();
                config.AddConsumer<UpdateAllReadMangaConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstants.UserIsCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<UserIsCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UpdateAllReadMangaQueue, c =>
                    {
                        c.ConfigureConsumer<UpdateAllReadMangaConsumer>(ctx);
                    });
                });                
            });


            return services;
        }

        public static IServiceCollection ConfigureMiscellaneousServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInformationRepository, UserInfoReposotory>();
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
    }
}
