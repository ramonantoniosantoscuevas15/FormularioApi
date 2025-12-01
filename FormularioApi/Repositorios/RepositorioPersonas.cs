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
        private readonly IMapper mapper;
        private readonly HttpContext httpContext;

        public RepositorioPersonas(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            return await queryable.Include(p => p.Telefonos).Include(p=>p.Correos).Include(p=>p.Dirreciones).
                Include(p=>p.CategoriaPersonas)
                .ThenInclude(cp => cp.Categoria).AsNoTracking().
                OrderByDescending(p => p.Apellido).Paginar(paginacionDTO).ToListAsync();
        }
        public async Task<List<Persona>> FiltrarCategoria(string tipo)
        {
            return await context.Personas.Include(p => p.CategoriaPersonas).ThenInclude(cp => cp.Categoria.Tipo).AsNoTracking()
               .ToListAsync();
                
        }

        public async Task<Persona?> ObtenerPorId(int id)
        {
            return await context.Personas.Include(p => p.CategoriaPersonas)
                .ThenInclude(cp => cp.Categoria).Include(p=> p.Telefonos).Include(p=> p.Dirreciones).
                Include(p => p.Correos)
                .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
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
            return await context.Personas.Where(p => p.Nombre.Contains(nombre)).Include(p => p.Telefonos).Include(p=>p.Correos).
                Include(p => p.Dirreciones).Include(p => p.CategoriaPersonas)
                .ThenInclude(cp => cp.Categoria).
                OrderBy(p => p.Nombre).ToListAsync();
        }
        public async Task Asignarcategoria (int id,List<int> categoriasIds)
        {
            var persona = await context.Personas.Include(p => p.CategoriaPersonas).FirstOrDefaultAsync(p => p.Id == id);

            if(persona is null)
            {
                throw new ArgumentException($"No existe una persona con el id: {id}");
            }
            var categoriapersonas = categoriasIds.Select(categoriaId => new CategoriaPersona() { CategoriaId = categoriaId, });
            persona.CategoriaPersonas = mapper.Map(categoriapersonas,persona.CategoriaPersonas);
            await context.SaveChangesAsync();
        }
        
    }
}
