using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioApi.Endpoints
{
    public static class TelefonosEndpoints
    {
        public static RouteGroupBuilder MapTelefonos(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Telefonos", ObtenerTelefonos).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("Telefonos-get"));
            group.MapGet("/Obtener Telefonos por id/{id:int}", ObtenerTelefonoPorId).WithName("ObtenerTelefonoporid");
            group.MapPost("/Agregar Telefono/persona/{personaId:int}/telefonos", AgregarTelefono);
            group.MapPut("/Actualizar Telefono/{id:int}", ActualizarTelefono);
            group.MapDelete("/Borrar Telefono/{id:int}", BorrarTelefono);
            return group;
        }
        static async Task<Results<Ok<List<TelefonoDTO>>, NotFound>> ObtenerTelefonos(int personaId,IRepositorioTelefonos repositorioTelefonos,
            IRepositorioPersonas repositorioPersonas, IMapper mapper)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var telefonos = await repositorioTelefonos.ObtenerTodos(personaId);
            var telefonosDTO = mapper.Map<List<TelefonoDTO>>(telefonos);
            return TypedResults.Ok(telefonosDTO);
        }
        static async Task<Results<Ok<TelefonoDTO>, NotFound>> ObtenerTelefonoPorId(IRepositorioTelefonos repositorio, 
            IMapper mapper, int id)
        {
            var telefonos = await repositorio.ObtenerPorId(id);
            if (telefonos is null)
            {
                return TypedResults.NotFound();
            }
            var telefonosDTO = mapper.Map<TelefonoDTO>(telefonos);
            return TypedResults.Ok(telefonosDTO );
        }
        static async Task<Results<CreatedAtRoute<TelefonoDTO>, NotFound>> AgregarTelefono(int personaId,CrearTelefonoDTO crearTelefonoDTO,
            IRepositorioTelefonos repositorioTelefonos, IMapper mapper, IOutputCacheStore outputCacheStore, IRepositorioPersonas repositorioPersonas)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var telefono = mapper.Map<Telefono>(crearTelefonoDTO);
            telefono.PersonaId = personaId;
            var id = await repositorioTelefonos.Crear(telefono);
            await outputCacheStore.EvictByTagAsync("Telefonos-get", default);
            var telefonoDTO = mapper.Map<TelefonoDTO>(telefono);
            return TypedResults.CreatedAtRoute(telefonoDTO, "ObtenerTelefonoporid", new { id, personaId });
        }
        static async Task<Results<NoContent, NotFound>> ActualizarTelefono(int personaId, int id,CrearTelefonoDTO crearTelefonoDTO,
            IRepositorioTelefonos repositorioTelefonos, IRepositorioPersonas repositorioPersonas, IMapper mapper,
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            if (!await repositorioTelefonos.Existe(id))
            {
                return TypedResults.NotFound();
            }
            var telefonos = mapper.Map<Telefono>(crearTelefonoDTO);
            telefonos.Id = id;
            telefonos.PersonaId = personaId;
            await repositorioTelefonos.Actualizar(telefonos);
            await outputCacheStore.EvictByTagAsync("telefonos-get", default);
            return TypedResults.NoContent();
        }
        static async Task<Results<NoContent, NotFound>> BorrarTelefono(int id, IRepositorioTelefonos repositorio, 
            IOutputCacheStore outputCacheStore)
        {
            if (!await repositorio.Existe(id))
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("telefonos-get", default);
            return TypedResults.NoContent();
        }
    }
}
