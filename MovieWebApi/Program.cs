using Microsoft.EntityFrameworkCore; // Importante
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.repository;
using MovieWebApi.Persistence.repository.interfaces;
using MovieWebApi.service.implementation;
using MovieWebApi.service.interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);



//Add instancias to the container.
// Add instancias to the container.
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieServiceServiceImpl>(); // ?? ESTA LÍNEA ES CLAVE

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
