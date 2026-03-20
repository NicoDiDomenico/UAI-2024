using Microsoft.EntityFrameworkCore;

namespace MindFit_Intelligence_Backend.Models
{
    public class MindFitIntelligenceContext : DbContext
    {
        public MindFitIntelligenceContext(DbContextOptions<MindFitIntelligenceContext> options)
            : base(options) { }

        public DbSet<PersonaResponsable> PersonaResponsables { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<PersonaSocio> PersonaSocios { get; set; } = null!;
        public DbSet<Grupo> Grupos { get; set; } = null!;
        public DbSet<Permiso> Permisos { get; set; } = null!;
        public DbSet<UsuarioGrupo> UsuarioGrupos { get; set; } = null!;
        public DbSet<GrupoPermiso> GrupoPermisos { get; set; } = null!;
        public DbSet<Rutina> Rutinas { get; set; } = null!;
        public DbSet<Dia> Dias { get; set; } = null!;
        public DbSet<PerfilIA> PerfilesIA { get; set; } = null!;
        public DbSet<Cuota> Cuotas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            - Regla de lo que coloco en el OnModelCreating:
                1. Configuraciones de tablas (ToTable)
                2. Relaciones (HasOne, WithOne, HasMany, WithMany, HasForeignKey)
            - El resto de las configuraciones (como índices, restricciones, etc) lo coloco en las clases de cada entidad usando Data Annotations.
             */
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonaResponsable>().ToTable("PersonaResponsable");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<PersonaSocio>().ToTable("PersonaSocio");
            modelBuilder.Entity<Grupo>().ToTable("Grupo");
            modelBuilder.Entity<Permiso>().ToTable("Permiso");
            modelBuilder.Entity<UsuarioGrupo>().ToTable("UsuarioGrupo");
            modelBuilder.Entity<GrupoPermiso>().ToTable("GrupoPermiso");
            modelBuilder.Entity<Rutina>().ToTable("Rutina");
            modelBuilder.Entity<Dia>().ToTable("Dia");
            modelBuilder.Entity<PerfilIA>().ToTable("PerfilIA");
            modelBuilder.Entity<Cuota>().ToTable("Cuota");

            //// Relacio 1 a 1 PK compartida
            // 1 a 1 PK compartida Usuario <-> PersonaResponsable
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PersonaResponsable)
                .WithOne(pr => pr.Usuario)
                .HasForeignKey<PersonaResponsable>(pr => pr.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 a 1 PK compartida Usuario <-> PersonaSocio
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PersonaSocio)
                .WithOne(ps => ps.Usuario)
                .HasForeignKey<PersonaSocio>(ps => ps.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            //// Modulo de Seguridad (N:M)
            // Relaciones UsuarioGrupo
            modelBuilder.Entity<UsuarioGrupo>()
                .HasOne(ug => ug.Usuario)
                .WithMany(u => u.UsuarioGrupos)
                .HasForeignKey(ug => ug.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioGrupo>()
                .HasOne(ug => ug.Grupo)
                .WithMany(g => g.UsuarioGrupos)
                .HasForeignKey(ug => ug.IdGrupo)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones GrupoPermiso
            modelBuilder.Entity<GrupoPermiso>()
                .HasOne(gp => gp.Grupo)
                .WithMany(g => g.GrupoPermisos)
                .HasForeignKey(gp => gp.IdGrupo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GrupoPermiso>()
                .HasOne(gp => gp.Permiso)
                .WithMany(p => p.GrupoPermisos)
                .HasForeignKey(gp => gp.IdPermiso);

            // Relaciones Rutina - PersonaSocio (N:1)
            modelBuilder.Entity<Rutina>()
                .HasOne(r => r.PersonaSocio)
                .WithMany(ps => ps.Rutinas)
                .HasForeignKey(r => r.IdPersonaSocio)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra un socio, se borran sus rutinas

            // Relacion Rutina - Dia (N:1)
            modelBuilder.Entity<Rutina>()
                .HasOne(r => r.Dia)
                .WithMany(d => d.Rutinas)
                .HasForeignKey(r => r.IdDia);

            // Relacion Cuota - PersonaSocio (N:1)
            modelBuilder.Entity<Cuota>()
                .HasOne(c => c.PersonaSocio)
                .WithMany(ps => ps.Cuotas)
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            //Configurando Enums para que se guarden como Strings en la BD (en vez de enteros)
            modelBuilder.Entity<PersonaResponsable>()
                .Property(p => p.Genero)
                .HasConversion<string>();

            modelBuilder.Entity<PersonaSocio>()
                .Property(p => p.EstadoSocio)
                .HasConversion<string>();

            modelBuilder.Entity<PersonaSocio>()
                .Property(p => p.Genero)
                .HasConversion<string>();

            modelBuilder.Entity<Cuota>()
                .Property(c => c.Plan)
                .HasConversion<string>();

            modelBuilder.Entity<Cuota>()
                .Property(c => c.EstadoCuota)
                .HasConversion<string>();

            // Configurando índice único para evitar que un mismo socio tenga más de una rutina para el mismo día
            // COLOCAR FLUENT VALIDADATION EN EL DTO DE RUTINA PARA VALIDAR QUE NO SE REPITA EL DIA EN LAS RUTINAS QUE ME LLEGAN EN EL DTO DE PERSONA SOCIO
            modelBuilder.Entity<Rutina>()
                .HasIndex(r => new { r.IdPersonaSocio, r.IdDia })
                .IsUnique();

            modelBuilder.Entity<PerfilIA>()
                .HasOne(p => p.PersonaSocio)
                .WithOne(ps => ps.PerfilIA)
                .HasForeignKey<PerfilIA>(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}