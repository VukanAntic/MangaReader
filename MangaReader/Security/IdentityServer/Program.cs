using IdentityServer.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// We replaced this with builder.Services.ConfigureJWT()
// builder.Services.AddAuthentication();

//Connecting to MongoDB
builder.Services.ConfigurePersistence(builder.Configuration);
// Mapping UserDTO - User and RoleDTO - Role
builder.Services.Mapper(builder.Configuration);
// Configure JWT
builder.Services.ConfigureJWT(builder.Configuration);
// Add other services
builder.Services.ConfigureMiscellaneousServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
