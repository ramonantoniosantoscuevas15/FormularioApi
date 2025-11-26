using FormularioApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi.Repositorios
{
    public class RepositorioPersonas : IRepositorioPersonas
    {
        private readonly ApplicationDbContext context;

        public RepositorioPersonas(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Personas.AnyAsync(p => p.Id == id);
        }
        public async Task<Persona?> ObtenerPorId(int id)
        {
            return await context.Personas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> Crear(Persona personas)
        {
            context.Add(personas);
            await context.SaveChangesAsync();
            return personas.Id;
        }
        public async Task Actualizar(Persona personas)
        {
            context.Update(personas);
            await context.SaveChangesAsync();

        }
        public async Task Borrar(int id)
        {
            await context.Personas.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Persona>> BusquedaPorNombre(string nombre)
        {
            return await context.Personas.Where(p => p.Nombre.Contains(nombre)).
                OrderBy(p => p.Nombre).ToListAsync();
        }
    }
}
