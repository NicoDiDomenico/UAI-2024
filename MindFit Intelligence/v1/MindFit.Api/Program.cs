using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindFit.Api.Data;
using MindFit.Api.Middleware;
using MindFit.Api.Services;
using MindFit.Api.Services.Auth;
using MindFit.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURACIÓN DE SERVICIOS =====

// 1. DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. HttpContextAccessor (requerido para multi-tenancy y permisos)
builder.Services.AddHttpContextAccessor();

// 3. Servicios de autenticación y permisos
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PermissionService>();

// 4. Configuración JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero // No tolerancia para expiración
    };
});

builder.Services.AddAuthorization();

// 5. Controllers
builder.Services.AddControllers();

// 6. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // Vite y React dev servers
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Importante para cookies HttpOnly
    });
});

// 7. Swagger con soporte JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MindFit Intelligence API",
        Version = "v1",
        Description = "API para gestión integral de gimnasios - Sistema SaaS B2B"
    });

    // Configurar JWT en Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ===== CONFIGURACIÓN DEL PIPELINE HTTP =====

// 1. Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MindFit API v1");
        options.RoutePrefix = string.Empty; // Swagger en raíz
    });
}

// 2. HTTPS Redirection
app.UseHttpsRedirection();

// 3. CORS
app.UseCors("AllowFrontend");

// 4. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 5. Middleware personalizado - Tenant Resolver
app.UseMiddleware<TenantResolverMiddleware>();

// 6. Mapear Controllers
app.MapControllers();

// ===== SEEDING DE DATOS =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Aplicar migraciones pendientes
        if (context.Database.GetPendingMigrations().Any())
        {
            Console.WriteLine("Aplicando migraciones pendientes...");
            context.Database.Migrate();
        }

        // Ejecutar seeding
        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error durante la inicialización de la base de datos");
    }
}

Console.WriteLine("\n=== MindFit Intelligence API ===");
Console.WriteLine("API iniciada correctamente");
Console.WriteLine($"Entorno: {app.Environment.EnvironmentName}");
Console.WriteLine("Swagger UI: https://localhost:7xxx (revisar puerto en launchSettings.json)");

app.Run();
