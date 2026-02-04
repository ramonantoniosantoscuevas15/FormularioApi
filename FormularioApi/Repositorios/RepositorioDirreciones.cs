using FormularioApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi.Repositorios
{
    public class RepositorioDirreciones : IRepositorioDirreciones
    {
        private readonly ApplicationDbContext context;

        public RepositorioDirreciones(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Dirreccion>> ObtenerTodos(int personaId)
        {
            return await context.Dirrecciones.Where(d => d.PersonaId == personaId).ToListAsync();
        }
        public async Task<Dirreccion?> ObtenerPorId(int id)
        {
            return await context.Dirrecciones.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<int> Crear(Dirreccion dirreciones)
        {
            context.Add(dirreciones);
            await context.SaveChangesAsync();
            return dirreciones.Id;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Dirrecciones.AnyAsync(d => d.Id == id);
        }
        public async Task Actualizar(Dirreccion dirrecion)
        {
            context.Update(dirrecion);
            await context.SaveChangesAsync();

        }
        public async Task Borrar(int id)
        {
            await context.Dirrecciones.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}
