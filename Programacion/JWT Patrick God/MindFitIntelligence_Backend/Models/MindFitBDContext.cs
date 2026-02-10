using Microsoft.EntityFrameworkCore;

namespace MindFitIntelligence_Backend.Models
{
    public class MindFitBDContext : DbContext 
    {
        public MindFitBDContext(DbContextOptions<MindFitBDContext> options) 
            : base(options) 
        { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}

/*
Para abrir la consola: 
    Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes

Crear una migración:
    Add-Migration InitialCreate
    Add-Migration AddTablaSocio -OutputDir Data\Migrations

Aplicar migraciones a la BD:
    Update-Database
    Update-Database AddTablaSocio          # hasta una migración específica
    Update-Database 0                      # vuelve al estado inicial (sin migraciones)

Listar migraciones:
    Get-Migration
    Get-Migration -Context GymContext

Quitar la última migración (si NO fue aplicada a la BD):
    Remove-Migration
    Remove-Migration -Force                # si querés forzar (cuidado)

Revertir la BD a una migración anterior (ya aplicada):
    Update-Database NombreMigracionAnterior

Borrar la base de datos:
    Drop-Database
    Drop-Database -Force

Generar script SQL de migraciones:
    Script-Migration                       # script desde la última aplicada hasta la última creada
    Script-Migration -From 0 -To AddTablaSocio -Idempotent -Output .\sql\deploy.sql

Generar script del modelo actual (sin migraciones):
    Script-DbContext -Output .\sql\create-model.sql

Info de DbContext disponibles:
    Get-DbContext

Scaffolding desde una BD existente (Database First):
    Scaffold-DbContext "Server=.;Database=GymDB;Trusted_Connection=True;TrustServerCertificate=True" `
      Microsoft.EntityFrameworkCore.SqlServer `
      -OutputDir Models -ContextDir Data -Context GymContext `
      -DataAnnotations -UseDatabaseNames -Force

Parámetros útiles (podés combinarlos):
    -Context GymContext → especifica un DbContext.
    -Project Backend → proyecto donde viven las migraciones.
    -StartupProject Api → proyecto que arranca la app (donde está Program.cs).
    -Environment Development → ambiente para Update-Database (si tu app usa variables por ambiente).
    -Verbose → salida detallada.

Ayuda rápida:
    Get-Help Add-Migration -Detailed
    Get-Help Update-Database -Detailed

*/