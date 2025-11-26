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

        protected ApplicationDbContext()
        {
        }
    }
}
