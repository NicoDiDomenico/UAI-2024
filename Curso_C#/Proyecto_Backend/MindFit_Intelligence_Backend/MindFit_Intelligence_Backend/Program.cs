using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MindFit_Intelligence_Backend.Automappers;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository;
using MindFit_Intelligence_Backend.Services;
using MindFit_Intelligence_Backend.DTOs.Usuarios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Services
builder.Services.AddScoped<IPersonaService<PersonaResponsableDto>, PersonaResponsableService>();
builder.Services.AddScoped<ICommonService<UsuarioResponsableDto, UsuarioResponsableInsertDto, UsuarioResponsableUpdateDto>, UsuarioService>();
//builder.Services.AddScoped<IAuthService, AuthService>();

// Repositories
builder.Services.AddScoped<IPersonaResponsableRepository, PersonaResponsableRepository>();
builder.Services.AddScoped<ICommonRepository<Usuario>, UsuarioRepository>();

// Entity Framework
builder.Services.AddDbContext<MindFitIntelligenceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);

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
