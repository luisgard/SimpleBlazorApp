using Microsoft.EntityFrameworkCore;
using SimpleModelos;

namespace SimpleDataAccess
{
    public class SimpleContexto : DbContext
    {
        public SimpleContexto()
        {
        }

        public SimpleContexto(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Aquí configuras la conexión a tu base de datos
                optionsBuilder.UseSqlite("Data Source=DbProductos.db"); // Cambia esto por tu cadena de conexión
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes definir configuraciones adicionales, como relaciones, índices, etc.
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);

            // Configuración de relación muchos a uno entre Producto y Categoria
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Cat) // Propiedad de navegación en Producto
                .WithMany() // No necesitamos propiedad de navegación en Categoria
                .HasForeignKey(p => p.CategoriaId); // Clave foránea en Producto

            // Otras configuraciones si las tienes

            base.OnModelCreating(modelBuilder);
        }
    }
}