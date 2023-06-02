using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.GRPC.Protos;
using Recommendation.API.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add GRPC
builder.Services.AddGrpcClient<MangaProtoService.MangaProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:MangaUrl"]));
builder.Services.AddScoped<MangaGrpcService>();
builder.Services.AddAutoMapper(configuration => {
    configuration.CreateMap<MangaDTO, Manga>().ReverseMap();
});
builder.Services.AddAutoMapper(configuration => {
    configuration.CreateMap<GenreDTO, GetMangaGenresResponse.Types.Genre>().ReverseMap();
});


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
