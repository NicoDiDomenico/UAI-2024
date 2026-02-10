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

            // 1 a 1 con PK compartida entre Usuario y PersonaResponsable
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PersonaResponsable) // Un Usuario tiene una PersonaResponsable
                .WithOne(pr => pr.Usuario) // Una PersonaResponsable tiene un Usuario
                .HasForeignKey<PersonaResponsable>(pr => pr.IdUsuario); // La PK de PersonaResponsable es también FK a Usuario

            // 1 a 1 con PK compartida entre Usuario y PersonaSocio
            modelBuilder.Entity<PersonaSocio>()
                .HasOne(ps => ps.Usuario)
                .WithOne(u => u.PersonaSocio)
                .HasForeignKey<PersonaSocio>(ps => ps.IdUsuario);
        }
    }
}
