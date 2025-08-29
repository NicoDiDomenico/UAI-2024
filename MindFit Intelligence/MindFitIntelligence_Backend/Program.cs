using Microsoft.EntityFrameworkCore;
using MindFitIntelligence_Backend.Automappers;
using MindFitIntelligence_Backend.DTOs;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Repository;
using MindFitIntelligence_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Mis Servicios
builder.Services.AddKeyedScoped<ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto>, UsuarioService>("usuarioService");

// Repository
builder.Services.AddKeyedScoped<IRepository<Usuario>, UsuarioRepository>("usuarioRepository");

// Entity Framework
builder.Services.AddDbContext<MindFitBDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
