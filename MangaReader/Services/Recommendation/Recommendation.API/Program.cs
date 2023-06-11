using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.GRPC.Protos;
using Recommendation.API.GrpcServices;
using UserInfo.GRPC.Protos;
using UserInfo.Common.Extentions;
using Recommendation.API.Contexts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add GRPC
builder.Services.AddGrpcClient<MangaProtoService.MangaProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:MangaUrl"]));
// TODO: fix this url, prb bad
builder.Services.AddGrpcClient<UserInfoProtoService.UserInfoProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:UserInfoUrl"]));
builder.Services.AddScoped<UserInfoGrpcService>();
builder.Services.AddScoped<MangaGrpcService>();
builder.Services.AddAutoMapper(configuration => {
    configuration.CreateMap<MangaDTO, Manga>().ReverseMap();
});
builder.Services.AddScoped<IRecommendationContext, RecommendationContext>();
//builder.Services.AddAutoMapper(configuration => {
//    configuration.CreateMap<GenreDTO, GetMangaGenresResponse.Types.Genre>().ReverseMap();
//});

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
