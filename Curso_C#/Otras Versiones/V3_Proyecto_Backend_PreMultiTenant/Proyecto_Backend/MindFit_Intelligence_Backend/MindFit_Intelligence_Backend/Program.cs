using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MindFit_Intelligence_Backend.Authorization;
using MindFit_Intelligence_Backend.Automappers;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.Repository;
using MindFit_Intelligence_Backend.Repository.Interfaces;
using MindFit_Intelligence_Backend.Services;
using MindFit_Intelligence_Backend.Services.Interfaces;
using FluentValidation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Services
builder.Services.AddScoped<IPersonaResponsableService, PersonaResponsableService>();
builder.Services.AddScoped<IRutinaService, RutinaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDiaService, DiaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IPermisoService, PermisoService>();
builder.Services.AddScoped<IFormularioService, FormularioService>();
builder.Services.AddScoped<IPersonaSocioService, PersonaSocioService>();
builder.Services.AddScoped<ICuotaService, CuotaService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();
builder.Services.AddScoped<IDiaRangoHorarioService, DiaRangoHorarioService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IMaquinaService, MaquinaService>();
builder.Services.AddScoped<IEquipamientoService, EquipamientoService>();
builder.Services.AddScoped<IEjercicioService, EjercicioService>();
builder.Services.AddScoped<IRangoHorarioService, RangoHorarioService>();
builder.Services.AddScoped<IGrupoMuscularRepository, GrupoMuscularRepository>();
builder.Services.AddScoped<ITipoEjercicioRepository, TipoEjercicioRepository>();

// Repositories
builder.Services.AddScoped<IPersonaResponsableRepository, PersonaResponsableRepository>();
builder.Services.AddScoped<IRutinaRepository, RutinaRepository>();
builder.Services.AddScoped<IPersonaSocioRepository, PersonaSocioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IFormularioRepository, FormularioRepository>();
builder.Services.AddScoped<IDiaRepository, DiaRepository>();
builder.Services.AddScoped<ICuotaRepository, CuotaRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
builder.Services.AddScoped<IDiaRangoHorarioRepository, DiaRangoHorarioRepository>();
builder.Services.AddScoped<IDiaRangoHorarioResponsableRepository, DiaRangoHorarioResponsableRepository>();
builder.Services.AddScoped<ICupoFechaRepository, CupoFechaRepository>();
builder.Services.AddScoped<IMaquinaRepository, MaquinaRepository>();
builder.Services.AddScoped<IEquipamientoRepository, EquipamientoRepository>();
builder.Services.AddScoped<IEjercicioRepository, EjercicioRepository>();
builder.Services.AddScoped<IRangoHorarioRepository, RangoHorarioRepository>();
builder.Services.AddScoped<IGrupoMuscularService, GrupoMuscularService>();
builder.Services.AddScoped<ITipoEjercicioService, TipoEjercicioService>();

// Entity Framework
builder.Services.AddDbContext<MindFitIntelligenceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);

// FluentValidation - Registro automático de todos los validadores del ensamblado
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Antes era solo builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto hace que los Enums (Genero, Plan, Estado) se envíen como Texto al Front
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Esto ayuda a que Swagger entienda que vas a usar Strings para los Enums
    c.DescribeAllParametersInCamelCase();
});

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

builder.Services.AddAuthorization(options =>
{
    //// Política para el Socio basada puramente en su Rol
    /// Módulo Gestión de Turno – Socio
    options.AddPolicy("SoloSocio", policy =>
        policy.RequireRole("Socio"));

    //// Política para el Responsable basada en Requerimientos
    /// Módulo Gestión de Turno – Asistente --> FORMULARIO_TURNOS_SOCIOS
    options.AddPolicy("AgregarTurno", policy =>
    policy.Requirements.Add(new PermisoRequirement("AGREGAR_TURNO")));

    options.AddPolicy("CancelarTurno", policy =>
        policy.Requirements.Add(new PermisoRequirement("CANCELAR_TURNO")));

    options.AddPolicy("ValidarIngreso", policy =>
        policy.Requirements.Add(new PermisoRequirement("VALIDAR_INGRESO")));
    // Usuario Socio
    options.AddPolicy("CrearUsuarioSocio", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_USUARIO_SOCIO")));

    options.AddPolicy("EditarUsuarioSocio", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_USUARIO_SOCIO")));

    options.AddPolicy("EliminarUsuarioSocio", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_USUARIO_SOCIO")));

    options.AddPolicy("EliminarUsuarioSocioDefinitivamente", policy =>
    policy.Requirements.Add(new PermisoRequirement("ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE")));
    // Auth
    options.AddPolicy("CambiarContrasenaSocio", policy =>
        policy.Requirements.Add(new PermisoRequirement("CAMBIAR_CONTRASENA_SOCIO")));

    /// Módulo Gestión del Gimnasio – Usuarios --> FORMULARIO_GYM_USUARIOS  
    // Usuario
    options.AddPolicy("CrearUsuarioResponsable", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_USUARIO_RESPONSABLE")));

    options.AddPolicy("EditarUsuarioResponsable", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_USUARIO_RESPONSABLE")));

    options.AddPolicy("EliminarUsuarioResponsableDefinitivamente", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE")));
    // Auth
    options.AddPolicy("CambiarContrasenaResponsable", policy =>
        policy.Requirements.Add(new PermisoRequirement("CAMBIAR_CONTRASENA_RESPONSABLE")));

    /// Módulo Gestión del Gimnasio – Permisos --> FORMULARIO_GYM_PERMISOS
    // Grupo
    options.AddPolicy("CrearGrupo", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_GRUPO")));

    options.AddPolicy("EditarGrupo", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_GRUPO")));

    options.AddPolicy("EliminarGrupo", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_GRUPO")));

    /// Módulo Gestión del Gimnasio – Equipamientos Máquinas y Ejercicios
    // Equipamiento --> FORMULARIO_EQUIPAMIENTOS
    options.AddPolicy("CrearEquipamiento", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_EQUIPAMIENTO"))); 

    options.AddPolicy("EditarEquipamiento", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_EQUIPAMIENTO")));

    options.AddPolicy("EliminarEquipamiento", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_EQUIPAMIENTO")));
    // Máquina --> FORMULARIO_MAQUINAS
    options.AddPolicy("CrearMaquina", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_MAQUINA")));

    options.AddPolicy("EditarMaquina", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_MAQUINA")));

    options.AddPolicy("EliminarMaquina", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_MAQUINA")));
    // Ejercicio --> FORMULARIO_EJERCICIOS
    options.AddPolicy("CrearEjercicio", policy =>
        policy.Requirements.Add(new PermisoRequirement("CREAR_EJERCICIO")));

    options.AddPolicy("EditarEjercicio", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_EJERCICIO")));

    options.AddPolicy("EliminarEjercicio", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_EJERCICIO")));

    /// Módulo Gestión del Gimnasio – Rangos Horarios --> FORMULARIO_GYM_HORARIOS
    options.AddPolicy("ModificarDiaRangoHorario", policy => 
    policy.Requirements.Add(new PermisoRequirement("MODIFICAR_DIA_RH")));

    options.AddPolicy("QuitarEntrenadorDiaRangoHorario", policy =>
        policy.Requirements.Add(new PermisoRequirement("QUITAR_ENTRENADOR_DIA_RH")));

    /// Módulo de Gestión de Rutinas --> FORMULARIO_RUTINAS
    // Rutina
    options.AddPolicy("EditarRutina", policy =>
        policy.Requirements.Add(new PermisoRequirement("EDITAR_RUTINA")));

    options.AddPolicy("VerHistorialRutina", policy =>
        policy.Requirements.Add(new PermisoRequirement("VER_HISTORIAL_RUTINA")));

    options.AddPolicy("EliminarRutina", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_RUTINA")));

    options.AddPolicy("RecuperarRutina", policy =>
        policy.Requirements.Add(new PermisoRequirement("RECUPERAR_RUTINA")));

    // No implementados en el front
    /*
    options.AddPolicy("EliminarCuota", policy =>
        policy.Requirements.Add(new PermisoRequirement("ELIMINAR_CUOTA")));
    */
});

builder.Services.AddScoped<IAuthorizationHandler, PermisoHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
