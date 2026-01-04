using Microsoft.EntityFrameworkCore;
using MindFit.Api.Models;

namespace MindFit.Api.Data;

/// <summary>
/// DbContext principal de la aplicación con soporte multi-tenancy
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor? httpContextAccessor = null)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // DbSets - Catálogo SaaS
    public DbSet<Gym> Gyms { get; set; }

    // DbSets - Seguridad
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<UsuarioGrupo> UsuarioGrupos { get; set; }
    public DbSet<GrupoPermiso> GrupoPermisos { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureGym(modelBuilder);
        ConfigureUsuario(modelBuilder);
        ConfigureGrupo(modelBuilder);
        ConfigurePermiso(modelBuilder);
        ConfigureUsuarioGrupo(modelBuilder);
        ConfigureGrupoPermiso(modelBuilder);
        ConfigureRefreshToken(modelBuilder);
        ConfigurePasswordResetToken(modelBuilder);

        // Aplicar Query Filters para multi-tenancy
        ApplyQueryFilters(modelBuilder);
    }

    private void ConfigureGym(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gym>(entity =>
        {
            entity.ToTable("Gyms");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).IsRequired();

            // Índices
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Activo);
        });
    }

    private void ConfigureUsuario(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.GymId).IsRequired();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).IsRequired();

            // Relación con Gym
            entity.HasOne(e => e.Gym)
                .WithMany(g => g.Usuarios)
                .HasForeignKey(e => e.GymId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            entity.HasIndex(e => new { e.GymId, e.Email }).IsUnique();
            entity.HasIndex(e => e.Activo);
        });
    }

    private void ConfigureGrupo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.ToTable("Grupos");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.GymId).IsRequired();
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.EsPredefinido).IsRequired().HasDefaultValue(false);
            entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).IsRequired();

            // Relación con Gym
            entity.HasOne(e => e.Gym)
                .WithMany()
                .HasForeignKey(e => e.GymId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            entity.HasIndex(e => new { e.GymId, e.Nombre }).IsUnique();
            entity.HasIndex(e => e.Activo);
        });
    }

    private void ConfigurePermiso(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.ToTable("Permisos");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Modulo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Activo).IsRequired().HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).IsRequired();

            // Índices
            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasIndex(e => e.Modulo);
        });
    }

    private void ConfigureUsuarioGrupo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioGrupo>(entity =>
        {
            entity.ToTable("UsuarioGrupos");
            entity.HasKey(e => new { e.UsuarioId, e.GrupoId });

            entity.Property(e => e.FechaAsignacion).IsRequired();

            // Relaciones
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.UsuarioGrupos)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Grupo)
                .WithMany(g => g.UsuarioGrupos)
                .HasForeignKey(e => e.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureGrupoPermiso(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GrupoPermiso>(entity =>
        {
            entity.ToTable("GrupoPermisos");
            entity.HasKey(e => new { e.GrupoId, e.PermisoId });

            entity.Property(e => e.FechaAsignacion).IsRequired();

            // Relaciones
            entity.HasOne(e => e.Grupo)
                .WithMany(g => g.GrupoPermisos)
                .HasForeignKey(e => e.GrupoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Permiso)
                .WithMany(p => p.GrupoPermisos)
                .HasForeignKey(e => e.PermisoId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureRefreshToken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UsuarioId).IsRequired();
            entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FechaExpiracion).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
            entity.Property(e => e.Revocado).IsRequired().HasDefaultValue(false);
            entity.Property(e => e.RevokedByIp).HasMaxLength(50);
            entity.Property(e => e.ReplacedByToken).HasMaxLength(500);

            // Relación con Usuario
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasIndex(e => e.FechaExpiracion);
            entity.HasIndex(e => e.Revocado);
        });
    }

    private void ConfigurePasswordResetToken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.ToTable("PasswordResetTokens");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UsuarioId).IsRequired();
            entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FechaExpiracion).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
            entity.Property(e => e.Usado).IsRequired().HasDefaultValue(false);

            // Relación con Usuario
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasIndex(e => e.FechaExpiracion);
            entity.HasIndex(e => e.Usado);
        });
    }

    /// <summary>
    /// Aplica Query Filters automáticos para multi-tenancy
    /// Filtra por GymId todas las entidades que lo requieren
    /// </summary>
    private void ApplyQueryFilters(ModelBuilder modelBuilder)
    {
        // Obtener GymId del HttpContext
        var gymId = GetCurrentGymId();

        // Query Filter para Usuario
        modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.GymId == gymId);

        // Query Filter para Grupo
        modelBuilder.Entity<Grupo>().HasQueryFilter(e => e.GymId == gymId);

        // Nota: Permiso NO tiene query filter porque es catálogo global
        // Nota: Las tablas de unión heredan el filtro de sus entidades principales
    }

    /// <summary>
    /// Obtiene el GymId actual del HttpContext
    /// Si no está disponible, retorna 0 (permitirá todas las consultas en contexto de seeding)
    /// </summary>
    private int GetCurrentGymId()
    {
        if (_httpContextAccessor?.HttpContext == null)
        {
            return 0; // En seeding o contexto sin HTTP
        }

        // Obtener GymId de HttpContext.Items (será establecido por TenantResolverMiddleware)
        if (_httpContextAccessor.HttpContext.Items.TryGetValue("GymId", out var gymIdObj) && gymIdObj is int gymId)
        {
            return gymId;
        }

        return 0;
    }
}
