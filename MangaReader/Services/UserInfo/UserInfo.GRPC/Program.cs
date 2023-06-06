using UserInfo.GRPC.Services;
using UserInfo.Common.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
// FIX NEEDED!
builder.Services.ConfigureReddisDataBase(builder.Configuration);
builder.Services.Mapper(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureMiscellaneousServices();
//builder.Services.AddUserInfoCommonServices(builder.Configuration);
// TODO: check if we need mapper, because it is never used

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserInfoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
