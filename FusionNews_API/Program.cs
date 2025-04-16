using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Common.Middleware;
using FusionNews_API.Helpers;
using FusionNews_API.Services.Jwt;
using FusionNews_API.WebExtensions;
using Infrastructure.EntityFramework.DataAccess;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddService();
builder.Services.AddRepository();
builder.Services.AddAutoMapper(typeof(MappingConfig));



builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    // Run this command in the terminal to generator migration
    // --> dotnet ef migrations add AddDB  -p ../Infrastructure -s ../FusionNews_Api 
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");


app.UseAuthorization();

app.MapControllers();

app.Run();
