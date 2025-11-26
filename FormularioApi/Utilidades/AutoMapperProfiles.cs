using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;

namespace FormularioApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //mapeando las personas
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<Persona, PersonaDTO>();
        }
    }
}
