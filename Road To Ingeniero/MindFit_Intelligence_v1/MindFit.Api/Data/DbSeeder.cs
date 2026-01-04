using Microsoft.EntityFrameworkCore;
using MindFit.Api.Constants;
using MindFit.Api.Models;
using BCrypt.Net;

namespace MindFit.Api.Data;

/// <summary>
/// Seeder de datos iniciales: Gym, Permisos, Grupos, Usuarios
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Verificar si ya hay datos
        if (await context.Gyms.AnyAsync())
        {
            Console.WriteLine("La base de datos ya contiene datos. Seeding omitido.");
            return;
        }

        Console.WriteLine("Iniciando seeding de datos...");

        // 1. Crear Gym de prueba
        var gym = new Gym
        {
            Nombre = "Gimnasio Demo",
            Email = "demo@mindfit.com",
            Telefono = "341-1234567",
            Direccion = "Av. Pellegrini 1234",
            Ciudad = "Rosario",
            Provincia = "Santa Fe",
            CodigoPostal = "S2000",
            FechaInicio = DateTime.UtcNow,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };
        context.Gyms.Add(gym);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ Gym creado: {gym.Nombre} (Id: {gym.Id})");

        // 2. Crear Permisos (catálogo global)
        var permisos = new List<Permiso>
        {
            // Módulo Socios
            new Permiso { Codigo = Permissions.SOCIOS_LISTAR, Nombre = "Listar Socios", Descripcion = "Ver listado de socios", Modulo = "Socios", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SOCIOS_VER_DETALLE, Nombre = "Ver Detalle Socio", Descripcion = "Ver información detallada de un socio", Modulo = "Socios", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SOCIOS_CREAR, Nombre = "Crear Socio", Descripcion = "Registrar nuevos socios", Modulo = "Socios", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SOCIOS_EDITAR, Nombre = "Editar Socio", Descripcion = "Modificar datos de socios", Modulo = "Socios", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SOCIOS_ELIMINAR, Nombre = "Eliminar Socio", Descripcion = "Eliminar socios del sistema", Modulo = "Socios", Activo = true, FechaCreacion = DateTime.UtcNow },
            
            // Módulo Turnos
            new Permiso { Codigo = Permissions.TURNOS_LISTAR, Nombre = "Listar Turnos", Descripcion = "Ver listado de turnos", Modulo = "Turnos", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.TURNOS_VER_DISPONIBLES, Nombre = "Ver Turnos Disponibles", Descripcion = "Consultar horarios disponibles", Modulo = "Turnos", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.TURNOS_RESERVAR, Nombre = "Reservar Turno", Descripcion = "Reservar turnos para socios", Modulo = "Turnos", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.TURNOS_CANCELAR, Nombre = "Cancelar Turno", Descripcion = "Cancelar reservas de turnos", Modulo = "Turnos", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.TURNOS_GESTIONAR, Nombre = "Gestionar Turnos", Descripcion = "Administración completa de turnos", Modulo = "Turnos", Activo = true, FechaCreacion = DateTime.UtcNow },
            
            // Módulo Rutinas
            new Permiso { Codigo = Permissions.RUTINAS_LISTAR, Nombre = "Listar Rutinas", Descripcion = "Ver listado de rutinas", Modulo = "Rutinas", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.RUTINAS_VER_DETALLE, Nombre = "Ver Detalle Rutina", Descripcion = "Ver información detallada de rutinas", Modulo = "Rutinas", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.RUTINAS_CREAR, Nombre = "Crear Rutina", Descripcion = "Crear nuevas rutinas de entrenamiento", Modulo = "Rutinas", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.RUTINAS_EDITAR, Nombre = "Editar Rutina", Descripcion = "Modificar rutinas existentes", Modulo = "Rutinas", Activo = true, FechaCreacion = DateTime.UtcNow },
            
            // Módulo Gimnasio
            new Permiso { Codigo = Permissions.GIMNASIO_MAQUINAS_LISTAR, Nombre = "Listar Máquinas", Descripcion = "Ver catálogo de máquinas", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.GIMNASIO_MAQUINAS_GESTIONAR, Nombre = "Gestionar Máquinas", Descripcion = "Administrar máquinas del gimnasio", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.GIMNASIO_EJERCICIOS_LISTAR, Nombre = "Listar Ejercicios", Descripcion = "Ver catálogo de ejercicios", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.GIMNASIO_EJERCICIOS_GESTIONAR, Nombre = "Gestionar Ejercicios", Descripcion = "Administrar ejercicios del gimnasio", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.GIMNASIO_CONFIG_GESTIONAR, Nombre = "Gestionar Configuración", Descripcion = "Administrar configuraciones del gimnasio", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.GIMNASIO_ENTRENADORES_GESTIONAR, Nombre = "Gestionar Entrenadores", Descripcion = "Administrar entrenadores del gimnasio", Modulo = "Gimnasio", Activo = true, FechaCreacion = DateTime.UtcNow },
            
            // Módulo Seguridad
            new Permiso { Codigo = Permissions.SEGURIDAD_USUARIOS_GESTIONAR, Nombre = "Gestionar Usuarios", Descripcion = "Administrar usuarios del sistema", Modulo = "Seguridad", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SEGURIDAD_GRUPOS_GESTIONAR, Nombre = "Gestionar Grupos", Descripcion = "Administrar grupos y permisos", Modulo = "Seguridad", Activo = true, FechaCreacion = DateTime.UtcNow },
            new Permiso { Codigo = Permissions.SEGURIDAD_PERMISOS_LISTAR, Nombre = "Listar Permisos", Descripcion = "Ver catálogo de permisos disponibles", Modulo = "Seguridad", Activo = true, FechaCreacion = DateTime.UtcNow },
            
            // Módulo IA
            new Permiso { Codigo = Permissions.IA_CHAT, Nombre = "Chat IA", Descripcion = "Acceso al asistente inteligente", Modulo = "IA", Activo = true, FechaCreacion = DateTime.UtcNow }
        };

        context.Permisos.AddRange(permisos);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ {permisos.Count} permisos creados");

        // 3. Crear Grupos predefinidos
        var grupos = new List<Grupo>
        {
            new Grupo { GymId = gym.Id, Nombre = Groups.ADMIN_GYM, Descripcion = "Administrador del gimnasio con acceso total", EsPredefinido = true, Activo = true, FechaCreacion = DateTime.UtcNow },
            new Grupo { GymId = gym.Id, Nombre = Groups.ADMIN_SEGURIDAD, Descripcion = "Administrador de seguridad y usuarios", EsPredefinido = true, Activo = true, FechaCreacion = DateTime.UtcNow },
            new Grupo { GymId = gym.Id, Nombre = Groups.ENTRENADOR, Descripcion = "Entrenador con gestión de rutinas", EsPredefinido = true, Activo = true, FechaCreacion = DateTime.UtcNow },
            new Grupo { GymId = gym.Id, Nombre = Groups.ASISTENTE, Descripcion = "Asistente con gestión básica", EsPredefinido = true, Activo = true, FechaCreacion = DateTime.UtcNow },
            new Grupo { GymId = gym.Id, Nombre = Groups.SOCIO, Descripcion = "Socio con acceso básico", EsPredefinido = true, Activo = true, FechaCreacion = DateTime.UtcNow }
        };

        context.Grupos.AddRange(grupos);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ {grupos.Count} grupos creados");

        // 4. Asignar permisos a grupos
        var grupoPermisos = new List<GrupoPermiso>();

        // ADMIN_GYM: Todos los permisos
        var adminGym = grupos.First(g => g.Nombre == Groups.ADMIN_GYM);
        foreach (var permiso in permisos)
        {
            grupoPermisos.Add(new GrupoPermiso { GrupoId = adminGym.Id, PermisoId = permiso.Id, FechaAsignacion = DateTime.UtcNow });
        }

        // ADMIN_SEGURIDAD: Permisos de seguridad
        var adminSeguridad = grupos.First(g => g.Nombre == Groups.ADMIN_SEGURIDAD);
        grupoPermisos.AddRange(permisos
            .Where(p => p.Modulo == "Seguridad")
            .Select(p => new GrupoPermiso { GrupoId = adminSeguridad.Id, PermisoId = p.Id, FechaAsignacion = DateTime.UtcNow }));

        // ENTRENADOR: Permisos de rutinas y socios (lectura)
        var entrenador = grupos.First(g => g.Nombre == Groups.ENTRENADOR);
        grupoPermisos.AddRange(permisos
            .Where(p => p.Modulo == "Rutinas" ||
                       (p.Modulo == "Socios" && (p.Codigo == Permissions.SOCIOS_LISTAR || p.Codigo == Permissions.SOCIOS_VER_DETALLE)))
            .Select(p => new GrupoPermiso { GrupoId = entrenador.Id, PermisoId = p.Id, FechaAsignacion = DateTime.UtcNow }));

        // ASISTENTE: Permisos de socios y turnos
        var asistente = grupos.First(g => g.Nombre == Groups.ASISTENTE);
        grupoPermisos.AddRange(permisos
            .Where(p => p.Modulo == "Socios" || p.Modulo == "Turnos")
            .Select(p => new GrupoPermiso { GrupoId = asistente.Id, PermisoId = p.Id, FechaAsignacion = DateTime.UtcNow }));

        // SOCIO: Permisos básicos (turnos propios, rutinas propias)
        var socio = grupos.First(g => g.Nombre == Groups.SOCIO);
        grupoPermisos.AddRange(permisos
            .Where(p => p.Codigo == Permissions.TURNOS_VER_DISPONIBLES ||
                       p.Codigo == Permissions.TURNOS_RESERVAR ||
                       p.Codigo == Permissions.TURNOS_CANCELAR ||
                       p.Codigo == Permissions.RUTINAS_LISTAR ||
                       p.Codigo == Permissions.RUTINAS_VER_DETALLE)
            .Select(p => new GrupoPermiso { GrupoId = socio.Id, PermisoId = p.Id, FechaAsignacion = DateTime.UtcNow }));

        context.GrupoPermisos.AddRange(grupoPermisos);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ {grupoPermisos.Count} permisos asignados a grupos");

        // 5. Crear usuarios de prueba
        var usuarios = new List<Usuario>
        {
            new Usuario
            {
                GymId = gym.Id,
                Email = "admin@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Nombre = "Admin",
                Apellido = "Sistema",
                Telefono = "341-0000001",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            },
            new Usuario
            {
                GymId = gym.Id,
                Email = "seguridad@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Seguridad123!"),
                Nombre = "Admin",
                Apellido = "Seguridad",
                Telefono = "341-0000002",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            },
            new Usuario
            {
                GymId = gym.Id,
                Email = "entrenador@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Entrenador123!"),
                Nombre = "Juan",
                Apellido = "Pérez",
                Telefono = "341-0000003",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            },
            new Usuario
            {
                GymId = gym.Id,
                Email = "asistente@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Asistente123!"),
                Nombre = "María",
                Apellido = "González",
                Telefono = "341-0000004",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            },
            new Usuario
            {
                GymId = gym.Id,
                Email = "socio@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Socio123!"),
                Nombre = "Carlos",
                Apellido = "Rodríguez",
                Telefono = "341-0000005",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            }
        };

        context.Usuarios.AddRange(usuarios);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ {usuarios.Count} usuarios creados");

        // 6. Asignar usuarios a grupos
        var usuarioGrupos = new List<UsuarioGrupo>
        {
            new UsuarioGrupo { UsuarioId = usuarios[0].Id, GrupoId = adminGym.Id, FechaAsignacion = DateTime.UtcNow },
            new UsuarioGrupo { UsuarioId = usuarios[1].Id, GrupoId = adminSeguridad.Id, FechaAsignacion = DateTime.UtcNow },
            new UsuarioGrupo { UsuarioId = usuarios[2].Id, GrupoId = entrenador.Id, FechaAsignacion = DateTime.UtcNow },
            new UsuarioGrupo { UsuarioId = usuarios[3].Id, GrupoId = asistente.Id, FechaAsignacion = DateTime.UtcNow },
            new UsuarioGrupo { UsuarioId = usuarios[4].Id, GrupoId = socio.Id, FechaAsignacion = DateTime.UtcNow }
        };

        context.UsuarioGrupos.AddRange(usuarioGrupos);
        await context.SaveChangesAsync();

        Console.WriteLine($"✓ {usuarioGrupos.Count} usuarios asignados a grupos");

        Console.WriteLine("\n=== SEEDING COMPLETADO ===");
        Console.WriteLine($"Gym: {gym.Nombre} (Id: {gym.Id})");
        Console.WriteLine($"Permisos: {permisos.Count}");
        Console.WriteLine($"Grupos: {grupos.Count}");
        Console.WriteLine($"Usuarios: {usuarios.Count}");
        Console.WriteLine("\nUsuarios de prueba:");
        Console.WriteLine("- admin@demo.com / Admin123! (ADMIN_GYM)");
        Console.WriteLine("- seguridad@demo.com / Seguridad123! (ADMIN_SEGURIDAD)");
        Console.WriteLine("- entrenador@demo.com / Entrenador123! (ENTRENADOR)");
        Console.WriteLine("- asistente@demo.com / Asistente123! (ASISTENTE)");
        Console.WriteLine("- socio@demo.com / Socio123! (SOCIO)");
    }
}
