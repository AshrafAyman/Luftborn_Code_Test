using Data.Extenstions;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Extenstions;
using SimpleCoursesCenterAPI.Extenstions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InjectAuthLayer();
builder.Services.InjectMvcLayer();
builder.Services.InjectCorsLayer();

var connectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString");
builder.Services.InjectDBContext(connectionString);

builder.Services.InjectServiceProjectServices();

// Add services to the container.
builder.Services.AddScoped<ISecurity, Security>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
