using MangaCatalog.Common.Data;
using MangaCatalog.Common.Repositories.Interfaces;
using MangaCatalog.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.AuthorPageResponse;
using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MassTransit;

namespace MangaCatalog.Common.Extensions
{
    public static class MangaCatalogCommonExtensions
    {
        public static void AddMangaCatalogCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMangaCatalogContext, MangaCatalogContext>();
            services.AddScoped<IMangaRepository, MangaRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<MangaDTO, Manga>().ReverseMap();
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<ChapterDTO, Chapter>().ReverseMap();
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<AuthorDTO, Author>().ReverseMap();
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<AuthorPageResponseDTO, AuthorPageResponse>().ReverseMap();
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<GenreDTO, Genre>().ReverseMap();
            });

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((_, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

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
