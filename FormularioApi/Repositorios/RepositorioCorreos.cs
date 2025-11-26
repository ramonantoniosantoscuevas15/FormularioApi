using FormularioApi.Entidades;
using FormularioApi.Migrations;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi.Repositorios
{
    public class RepositorioCorreos : IRepositorioCorreos
    {
        private readonly ApplicationDbContext context;

        public RepositorioCorreos(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Correo>> ObtenerTodos(int personaId)
        {
            return await context.Correos.Where(c => c.PersonaId == personaId).ToListAsync();
        }
        public async Task<Correo?> ObtenerPorId(int id)
        {
            return await context.Correos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<int> Crear(Correo correos)
        {
            context.Add(correos);
            await context.SaveChangesAsync();
            return correos.Id;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Correos.AnyAsync(c => c.Id == id);
        }
        public async Task Actualizar(Correo correos)
        {
            context.Update(correos);
            await context.SaveChangesAsync();

        }
        public async Task Borrar(int id)
        {
            await context.Correos.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
