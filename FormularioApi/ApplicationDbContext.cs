using FormularioApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Correo> Correos { get; set; }
        public DbSet<Dirrecion> Dirreciones { get; set; }

        protected ApplicationDbContext()
        {
        }
    }
}
