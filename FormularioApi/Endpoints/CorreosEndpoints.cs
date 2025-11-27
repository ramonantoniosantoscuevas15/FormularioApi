using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioApi.Endpoints
{
    public static class CorreosEndpoints
    {
        public static RouteGroupBuilder MapCorreos(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Correos", ObtenerCorreos).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("Correos-get"));
            group.MapGet("/Obtener Correos por id/{id:int}", ObtenerCorreoPorId).WithName("ObtenerCorreoporid");
            group.MapPost("/Agregar Correo/persona/{personaId:int}/correos", AgregarCorreo);
            group.MapPut("/Actualizar Correo/{id:int}", ActualizarCorreo);
            group.MapDelete("/Borrar Correo/{id:int}", BorrarCorreo);
            return group;
        }
        static async Task<Results<Ok<List<CorreoDTO>>, NotFound>> ObtenerCorreos(int personaId, IRepositorioCorreos repositorioCorreos,
            IRepositorioPersonas repositorioPersonas, IMapper mapper)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var correos = await repositorioCorreos.ObtenerTodos(personaId);
            var correosDTO = mapper.Map<List<CorreoDTO>>(correos);
            return TypedResults.Ok(correosDTO);
        }
        static async Task<Results<Ok<CorreoDTO>, NotFound>> ObtenerCorreoPorId ( IRepositorioCorreos repositorio,
            IMapper mapper,int id)
        {
            var correos = await repositorio.ObtenerPorId(id);
            if (correos is null) 
            {
                return TypedResults.NotFound();
            }
            var correoDTO = mapper.Map<CorreoDTO>(correos);
            return TypedResults.Ok(correoDTO);

        }
        static async Task<Results<CreatedAtRoute<CorreoDTO>, NotFound>> AgregarCorreo (int personaId, CrearCorreoDTO crearcorreoDTO,
            IRepositorioCorreos repositorioCorreos, IRepositorioPersonas repositorioPersonas,IMapper mapper, IOutputCacheStore outputCacheStore)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var correo = mapper.Map<Correo>(crearcorreoDTO);
            correo.PersonaId = personaId;
            var id = await repositorioCorreos.Crear(correo);
            await outputCacheStore.EvictByTagAsync("Correos-get",default);
            var correoDTO = mapper.Map<CorreoDTO>(correo);
            return TypedResults.CreatedAtRoute(correoDTO,"ObtenerCorreoporId", new {id,personaId});
        }
        static async Task<Results<NoContent, NotFound>> ActualizarCorreo(int personaId,int id,CrearCorreoDTO crearCorreoDTO,
            IRepositorioCorreos repositorioCorreos,IRepositorioPersonas repositorioPersonas,IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            if (!await repositorioCorreos.Existe(id))
            {
                return TypedResults.NotFound();
            }
            var correos = mapper.Map<Correo>(crearCorreoDTO);
            correos.Id = id;
            correos.PersonaId = personaId;
            await repositorioCorreos.Actualizar(correos);
            await outputCacheStore.EvictByTagAsync("correos-get", default);
            return TypedResults.NoContent();

        }
        static async Task<Results<NoContent, NotFound>> BorrarCorreo(int id, IRepositorioCorreos repositorio, 
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorio.Existe(id))
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("correos-get", default);
            return TypedResults.NoContent();
        }

    }
}
