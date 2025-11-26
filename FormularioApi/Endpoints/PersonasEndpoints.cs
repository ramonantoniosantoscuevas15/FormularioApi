using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;
using FormularioApi.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioApi.Endpoints
{
    public static class PersonasEndpoints
    {
        public static RouteGroupBuilder MapPersonas(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener persona por id/{id:int}", ObtenerpersonaPorId);
            group.MapPost("/Agregar Personas", AgregarPersona);
            group.MapPut("/Actualizar Personas/{id:int}", ActualizarPersona);
            group.MapDelete("/Borrar Personas/{id:int}", BorrarPersona);
            return group;

        }
        static async Task<Results<Ok<PersonaDTO>,NotFound>> ObtenerpersonaPorId(IRepositorioPersonas repositorio, int id, IMapper mapper)
        {
            var persona = await repositorio.ObtenerPorId(id);
            if (persona is null)
            {
                return TypedResults.NotFound();
            }
            var personaDTO = mapper.Map<PersonaDTO>(persona);
            return TypedResults.Ok(personaDTO);
        }
        static async Task<Created<PersonaDTO>> AgregarPersona(CrearPersonaDTO CrearpersonaDTO, IRepositorioPersonas repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var personas = mapper.Map<Persona>(CrearpersonaDTO);
            var id = await repositorio.Crear(personas);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            var personaDTO = mapper.Map<PersonaDTO>(personas);
            return TypedResults.Created($"/personas/{id}", personaDTO);
        }
        static async Task<Results<NoContent, NotFound>> ActualizarPersona(int id, CrearPersonaDTO CrearpersonaDTO, IRepositorioPersonas repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var personas = mapper.Map<Persona>(CrearpersonaDTO);
            personas.Id = id;
            await repositorio.Actualizar(personas);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            return TypedResults.NoContent();
        }
        static async Task<Results<NoContent, NotFound>> BorrarPersona(int id, IRepositorioPersonas repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            return TypedResults.NoContent();
        }

    }
}
