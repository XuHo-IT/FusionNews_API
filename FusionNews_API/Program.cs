using Common.Middleware;
using FusionNews_API.Data;
using FusionNews_API.Helpers;
using FusionNews_API.Models;
using FusionNews_API.WebExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FusionNews_API.Services.Jwt; //Added to initialize JWT key

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<JwtService>();
builder.Services.AddService();
builder.Services.AddRepository();
builder.Services.AddAutoMapper(typeof(MappingConfig));



builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        opts => opts.CommandTimeout(120));
});

//Builder Service for UserDBContext
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Username=sa;Password=123456;Database=FusionNewsDB");
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

app.UseAuthorization();

app.MapControllers();

app.Run();
