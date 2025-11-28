using FormularioApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaPersona>().HasKey(c=> new {c.CategoriaId,c.PersonaId});
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Correo> Correos { get; set; }
        public DbSet<Dirrecion> Dirreciones { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaPersona> CategoriaPersonas { get; set; }

        protected ApplicationDbContext()
        {
        }
    }
}
