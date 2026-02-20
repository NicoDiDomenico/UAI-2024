using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MindFit_Intelligence_Backend.Automappers;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository;
using MindFit_Intelligence_Backend.Services;
using MindFit_Intelligence_Backend.Services.Email;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Services
builder.Services.AddScoped<IPersonaService<PersonaResponsableDto>, PersonaResponsableService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();

// Repositories
builder.Services.AddScoped<IPersonaResponsableRepository, PersonaResponsableRepository>();
builder.Services.AddScoped<IPersonaSocioRepository, PersonaSocioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();

// Entity Framework
builder.Services.AddDbContext<MindFitIntelligenceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);

// Inyección de dependencias para el servicio de email (SMTP)
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Se configura cómo se validarán los tokens JWT que lleguen a la API.
    .AddJwtBearer(options =>
    {
        // Se establecen los parámetros que definirán las reglas de validación del token.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Indica que se debe validar quién emitió el token (Issuer).
            ValidateIssuer = true,

            // Define el emisor válido del token (por ejemplo, tu propio servidor o aplicación).
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],

            // Indica que se debe validar la audiencia del token (para quién fue emitido).
            ValidateAudience = true,

            // Define la audiencia válida (por ejemplo, tu frontend o aplicación cliente).
            ValidAudience = builder.Configuration["AppSettings:Audience"],

            // Indica que se debe verificar que el token no haya expirado.
            ValidateLifetime = true,

            // Clave secreta usada para validar la firma del token.
            // Debe ser la misma que se usó para generarlo.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),

            // Indica que se debe validar la firma del token (para evitar tokens falsificados).
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
