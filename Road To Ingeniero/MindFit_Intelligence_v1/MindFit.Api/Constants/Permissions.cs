namespace MindFit.Api.Constants;

/// <summary>
/// Constantes de permisos del sistema
/// </summary>
public static class Permissions
{
    // Módulo Socios
    public const string SOCIOS_LISTAR = "SOCIOS_LISTAR";
    public const string SOCIOS_VER_DETALLE = "SOCIOS_VER_DETALLE";
    public const string SOCIOS_CREAR = "SOCIOS_CREAR";
    public const string SOCIOS_EDITAR = "SOCIOS_EDITAR";
    public const string SOCIOS_ELIMINAR = "SOCIOS_ELIMINAR";

    // Módulo Turnos
    public const string TURNOS_LISTAR = "TURNOS_LISTAR";
    public const string TURNOS_VER_DISPONIBLES = "TURNOS_VER_DISPONIBLES";
    public const string TURNOS_RESERVAR = "TURNOS_RESERVAR";
    public const string TURNOS_CANCELAR = "TURNOS_CANCELAR";
    public const string TURNOS_GESTIONAR = "TURNOS_GESTIONAR";

    // Módulo Rutinas
    public const string RUTINAS_LISTAR = "RUTINAS_LISTAR";
    public const string RUTINAS_VER_DETALLE = "RUTINAS_VER_DETALLE";
    public const string RUTINAS_CREAR = "RUTINAS_CREAR";
    public const string RUTINAS_EDITAR = "RUTINAS_EDITAR";

    // Módulo Gimnasio
    public const string GIMNASIO_MAQUINAS_LISTAR = "GIMNASIO_MAQUINAS_LISTAR";
    public const string GIMNASIO_MAQUINAS_GESTIONAR = "GIMNASIO_MAQUINAS_GESTIONAR";
    public const string GIMNASIO_EJERCICIOS_LISTAR = "GIMNASIO_EJERCICIOS_LISTAR";
    public const string GIMNASIO_EJERCICIOS_GESTIONAR = "GIMNASIO_EJERCICIOS_GESTIONAR";
    public const string GIMNASIO_CONFIG_GESTIONAR = "GIMNASIO_CONFIG_GESTIONAR";
    public const string GIMNASIO_ENTRENADORES_GESTIONAR = "GIMNASIO_ENTRENADORES_GESTIONAR";

    // Módulo Seguridad
    public const string SEGURIDAD_USUARIOS_GESTIONAR = "SEGURIDAD_USUARIOS_GESTIONAR";
    public const string SEGURIDAD_GRUPOS_GESTIONAR = "SEGURIDAD_GRUPOS_GESTIONAR";
    public const string SEGURIDAD_PERMISOS_LISTAR = "SEGURIDAD_PERMISOS_LISTAR";

    // Módulo IA
    public const string IA_CHAT = "IA_CHAT";
}
