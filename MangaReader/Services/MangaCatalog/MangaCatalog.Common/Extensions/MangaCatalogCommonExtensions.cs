using MangaCatalog.Common.Data;
using MangaCatalog.Common.Repositories.Interfaces;
using MangaCatalog.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.AuthorPageResponse;
using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Entities;

namespace MangaCatalog.Common.Extensions
{
    public static class MangaCatalogCommonExtensions
    {
        public static void AddMangaCatalogCommonServices(this IServiceCollection services)
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
        }
    }
}
