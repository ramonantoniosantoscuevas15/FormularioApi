using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioApi.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static RouteGroupBuilder MapCategorias(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Catalogo", ObtenerCategorias).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("categorias-get"));
            group.MapGet("/Obtener Categoria por id/{id:int}", ObtenerCategoriaPorId);
            group.MapPost("/Agregar Categoria", AgregarCategoria);
            group.MapPut("/Actualizar Categoria/{id:int}", ActualizarCategoria);
            group.MapDelete("/Borrar Categoria/{id:int}", BorrarCategoria);
            return group;
        }
        static async Task<Ok<List<CategoriaDTO>>> ObtenerCategorias(IRepositorioCategorias repositorio,IMapper mapper, 
            int pagina = 1, int recordsPorPagina = 10)
        {
            var paginacion = new PaginacionDTO { Pagina = pagina, RecordsPorPagina = recordsPorPagina };
            var categorias = await repositorio.ObtenerTodos(paginacion);
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);
            return TypedResults.Ok(categoriasDTO);
        }
        static async Task<Results<Ok<CategoriaDTO>, NotFound>>ObtenerCategoriaPorId(IRepositorioCategorias repositorio,IMapper mapper,int id)
        {
            var categoria = await repositorio.ObtenerPorId(id);
            if(categoria is null)
            {
                return TypedResults.NotFound();
            }
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return TypedResults.Ok(categoriaDTO);
        }
        static async Task<Created<CategoriaDTO>> AgregarCategoria(CrearCategoriaDTO crearCategoriaDTO,IRepositorioCategorias repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var categorias = mapper.Map<Categoria>(crearCategoriaDTO);
            var id = await repositorio.Crear(categorias);
            await outputCacheStore.EvictByTagAsync("categorias-get", default);
            var categoriaDTO = mapper.Map<CategoriaDTO> (categorias);
            return TypedResults.Created($"/categorias/{id}",categoriaDTO);

        }
        static async Task<Results<NoContent, NotFound>> ActualizarCategoria(int id, CrearCategoriaDTO crearCategoriaDTO, IRepositorioCategorias repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var categorias = mapper.Map<Categoria> (crearCategoriaDTO);
            categorias.Id = id; 
            await repositorio.Actualizar(categorias);
            await outputCacheStore.EvictByTagAsync("categorias-get", default);
            return TypedResults.NoContent();

        }
        static async Task<Results<NoContent, NotFound>> BorrarCategoria(int id, IRepositorioCategorias repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("categorias-gett", default);
            return TypedResults.NoContent();
        }
    }
}
