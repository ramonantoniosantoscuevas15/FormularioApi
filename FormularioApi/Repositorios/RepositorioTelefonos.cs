using FormularioApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi.Repositorios
{
    public class RepositorioTelefonos : IRepositorioTelefonos
    {
        private readonly ApplicationDbContext context;

        public RepositorioTelefonos(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Telefono>> ObtenerTodos(int personaId)
        {
            return await context.Telefonos.Where(t => t.PersonaId == personaId).ToListAsync();
        }
        public async Task<Telefono?> ObtenerPorId(int id)
        {
            return await context.Telefonos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<int> Crear(Telefono telefonos)
        {
            context.Add(telefonos);
            await context.SaveChangesAsync();
            return telefonos.Id;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Telefonos.AnyAsync(t => t.Id == id);
        }
        public async Task Actualizar(Telefono telefonos)
        {
            context.Update(telefonos);
            await context.SaveChangesAsync();

        }
        public async Task Borrar(int id)
        {
            await context.Telefonos.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

    }
}
