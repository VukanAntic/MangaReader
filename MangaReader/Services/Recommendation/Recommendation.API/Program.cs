using MangaCatalog.GRPC.Protos;
using MangaCatalog.Common.Extensions;
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
builder.Services.AddMangaGrpcMaps(builder.Configuration);

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
