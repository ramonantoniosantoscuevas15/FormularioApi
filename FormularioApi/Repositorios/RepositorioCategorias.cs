using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FormularioApi.Repositorios
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly ApplicationDbContext context;
        private readonly HttpContext httpContext;

        public RepositorioCategorias(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Categorias.AnyAsync(c => c.Id == id);
        }
        public async Task<List<int>> Existen(List<int> ids)
        {
            return await context.Categorias.Where(c => ids.Contains(c.Id)).Select(c => c.Id).ToListAsync();
        }
        public async Task<List<Categoria>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            var queryable = context.Categorias.AsQueryable();
            await httpContext.InsertarParametrosPaginacionEncabecera(queryable);
            return await queryable.OrderBy(c => c.Tipo).Paginar(paginacionDTO).ToListAsync();
        }
        public async Task<Categoria?> ObtenerPorId(int id)
        {
            return await context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<int> Crear(Categoria categorias)
        {
            context.Add(categorias);
            await context.SaveChangesAsync();
            return categorias.Id;

        }
        public async Task Actualizar(Categoria categorias)
        {
            context.Update(categorias);
            await context.SaveChangesAsync();
        }
        public async Task Borrar(int id)
        {
            await context.Categorias.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

    }
}
