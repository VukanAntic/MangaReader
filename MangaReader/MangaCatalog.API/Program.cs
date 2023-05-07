using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories;
using MangaCatalog.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMangaCatalogContext, MangaCatalogContext>();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<DescriptiveMangaDTO, Manga>().ReverseMap();
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
