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
            //mapeando los correos
            CreateMap<CrearCorreoDTO, Correo>();
            CreateMap<Correo, CorreoDTO>();
            //mapeando las dirreciones
            CreateMap<CrearDirrecionDTO, Dirrecion>();
            CreateMap<Dirrecion, DirrecionDTO>();
            //mapeando los telefonos
            CreateMap<CrearTelefonoDTO, Telefono>();
            CreateMap<Telefono, TelefonoDTO>();
        }
    }
}
