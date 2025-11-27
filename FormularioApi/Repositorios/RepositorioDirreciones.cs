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
        public async Task<List<Dirrecion>> ObtenerTodos(int personaId)
        {
            return await context.Dirreciones.Where(d => d.PersonaId == personaId).ToListAsync();
        }
        public async Task<Dirrecion?> ObtenerPorId(int id)
        {
            return await context.Dirreciones.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<int> Crear(Dirrecion dirreciones)
        {
            context.Add(dirreciones);
            await context.SaveChangesAsync();
            return dirreciones.Id;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Dirreciones.AnyAsync(d => d.Id == id);
        }
        public async Task Actualizar(Dirrecion dirrecion)
        {
            context.Update(dirrecion);
            await context.SaveChangesAsync();

        }
        public async Task Borrar(int id)
        {
            await context.Dirreciones.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}
