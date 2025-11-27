using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace FormularioApi.Repositorios
{
    public class RepositorioPersonas : IRepositorioPersonas
    {
        private readonly ApplicationDbContext context;
        private readonly HttpContext httpContext;

        public RepositorioPersonas(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Personas.AnyAsync(p => p.Id == id);
        }
        public async Task<List<Persona>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            //orden de los nombres por apellido de forma decendente
            //return await context.Personas.OrderBy(p => p.Apellido).ToListAsync();
            var queryable = context.Personas.AsQueryable();
            await httpContext.InsertarParametrosPaginacionEncabecera(queryable);
            return await queryable.Include(p=>p.Correos).Include(p=>p.Dirreciones).
                OrderByDescending(p => p.Apellido).Paginar(paginacionDTO).ToListAsync();
        }
        public async Task<Persona?> ObtenerPorId(int id)
        {
            return await context.Personas.Include(p => p.Correos).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
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
