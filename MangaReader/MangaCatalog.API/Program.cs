using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.DTOs.AuthorPageResponse;
using MangaCatalog.API.DTOs.Chapter;
using MangaCatalog.API.DTOs.Manga;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories;
using MangaCatalog.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMangaCatalogContext, MangaCatalogContext>();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<MangaDTO, Manga>().ReverseMap();
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<ChapterDTO, Chapter>().ReverseMap();
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<AuthorDTO, Author>().ReverseMap();
});

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<AuthorPageResponseDTO, AuthorPageResponse>().ReverseMap();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
