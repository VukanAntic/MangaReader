using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Extensions;
using MangaCatalog.GRPC.Protos;
using MangaCatalog.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddMangaCatalogCommonServices(builder.Configuration);
builder.Services.AddAutoMapper(configuration => {
    configuration.CreateMap<MangaDTO, Manga>().ReverseMap();
});
builder.Services.AddAutoMapper(configuration => {
    configuration.CreateMap<GenreDTO, GetMangaGenresResponse.Types.Genre>().ReverseMap();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MangaService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
