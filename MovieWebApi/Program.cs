using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore; // Importante
using Microsoft.IdentityModel.Tokens;
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.repository;
using MovieWebApi.Persistence.repository.interfaces;
using MovieWebApi.service.implementation;
using MovieWebApi.service.interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();









// Add instancias to the container.
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<IMovieService, MovieServiceServiceImpl>(); // ?? ESTA LÍNEA ES CLAVE

builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.

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

    app.UseAuthentication();
    app.UseAuthorization();


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
