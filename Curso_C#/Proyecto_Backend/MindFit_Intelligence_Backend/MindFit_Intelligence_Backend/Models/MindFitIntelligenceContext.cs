using Microsoft.EntityFrameworkCore;

namespace MindFit_Intelligence_Backend.Models
{
    public class MindFitIntelligenceContext : DbContext
    {
        public MindFitIntelligenceContext(DbContextOptions<MindFitIntelligenceContext> options)
            : base(options) {}

        public DbSet<PersonaResponsable> PersonaResponsables { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PersonaSocio> PersonaSocios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonaResponsable>().ToTable("PersonaResponsable");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<PersonaSocio>().ToTable("PersonaSocio");

            // 1 a 1 PK compartida Usuario <-> PersonaResponsable
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PersonaResponsable)
                .WithOne(pr => pr.Usuario)
                .HasForeignKey<PersonaResponsable>(pr => pr.IdUsuario);

            // 1 a 1 PK compartida Usuario <-> PersonaSocio
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PersonaSocio)
                .WithOne(ps => ps.Usuario)
                .HasForeignKey<PersonaSocio>(ps => ps.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
