using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MindFitIntelligence_Backend.Automappers;
using MindFitIntelligence_Backend.DTOs;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Repository;
using MindFitIntelligence_Backend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Mis Servicios
builder.Services.AddKeyedScoped<ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto>, UsuarioService>("usuarioService");
builder.Services.AddKeyedScoped<IAuthService, AuthService>("authService");
// Se agrega el servicio de autenticación al contenedor de dependencias.
// Se indica que el esquema de autenticación a usar será JWT (Json Web Token).
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


// Middlewares
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication(); // JWT
app.UseAuthorization(); // JWT

app.MapControllers();

app.Run();
