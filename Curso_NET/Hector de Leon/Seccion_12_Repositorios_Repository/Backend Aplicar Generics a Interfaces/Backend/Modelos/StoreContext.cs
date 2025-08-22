using Microsoft.EntityFrameworkCore;

namespace Backend.Modelos
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; } // Tabla de cervezas
        public DbSet<Brand> Brands { get; set; } // Tabla de marcas
        // Hay ciertas opciones que permiten no escribirlo en plural en la base de datos, ya que por defecto EF Core crea las tablas en plural.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .HasOne(b => b.Brand) // Relación uno a muchos
                .WithMany() // Una marca puede tener muchas cervezas
                .HasForeignKey(b => b.BrandID); // Clave foránea en Beer
        }
    }
}
