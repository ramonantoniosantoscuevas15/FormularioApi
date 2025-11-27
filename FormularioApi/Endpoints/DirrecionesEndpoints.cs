using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioApi.Endpoints
{
    public static class DirrecionesEndpoints
    {
        public static RouteGroupBuilder MapDirreciones(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Dirreciones", ObtenerDirreciones).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("Dirrecciones-get"));
            group.MapGet("/Obtener Dirreciones por id/{id:int}", ObtenerDirrecionPorId).WithName("ObtenerDirrecionporId");
            group.MapPost("/Agregar Dirrecion/persona/{personaId:int}/dirrecion", AgregarDirrecion);
            group.MapPut("/Actualizar Dirrecion/{id:int}", ActualizarDirrecion);
            group.MapDelete("/Borrar Dirrecion/{id:int}", BorrarDirrecion);

            return group;
        }
        static async Task<Results<Ok<List<DirrecionDTO>>, NotFound>> ObtenerDirreciones(int personaId,IRepositorioDirreciones repositorioDirreciones,
            IRepositorioPersonas repositorioPersonas,IMapper mapper)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var dirreciones = await repositorioDirreciones.ObtenerTodos(personaId);
            var dirrecionesDTO = mapper.Map<List<DirrecionDTO>>(dirreciones);
            return TypedResults.Ok(dirrecionesDTO);
        }
        static async Task<Results<Ok<DirrecionDTO>, NotFound>> ObtenerDirrecionPorId(IRepositorioDirreciones repositorio, 
            IMapper mapper, int id)
        {
            var dirreciones = await repositorio.ObtenerPorId(id);
            if(dirreciones is null)
            {
                return TypedResults.NotFound();
            }
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirreciones);
            return TypedResults.Ok(dirrecionDTO);
        }
        static async Task<Results<CreatedAtRoute<DirrecionDTO>, NotFound>> AgregarDirrecion(int personaId,IRepositorioDirreciones repositorioDirreciones,
            IRepositorioPersonas repositorioPersonas, IMapper mapper, IOutputCacheStore outputCacheStore, CrearDirrecionDTO crearDirrecionDTO)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var dirrecion = mapper.Map<Dirrecion>(crearDirrecionDTO);
            dirrecion.PersonaId = personaId;
            var id = await repositorioDirreciones.Crear(dirrecion);
            await outputCacheStore.EvictByTagAsync("Dirreciones-get", default);
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirrecion);
            return TypedResults.CreatedAtRoute(dirrecionDTO, "ObtenerDirrecionporId", new { id, personaId });
        }
        static async Task<Results<NoContent, NotFound>> ActualizarDirrecion(int personaId,int id,CrearDirrecionDTO crearDirrecionDTO,
            IRepositorioDirreciones repositorioDirreciones, IRepositorioPersonas repositorioPersonas, IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            if(!await repositorioDirreciones.Existe(id))
            {
                return TypedResults.NotFound();
            }
            var dirreciones = mapper.Map<Dirrecion>(crearDirrecionDTO);
            dirreciones.Id = id;
            dirreciones.PersonaId = personaId;
            await repositorioDirreciones.Actualizar(dirreciones);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            return TypedResults.NotFound();
        }
        static async Task<Results<NoContent, NotFound>> BorrarDirrecion(int id, IRepositorioDirreciones repositorio, 
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorio.Existe(id))
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            return TypedResults.NoContent();
        }
    }
}
