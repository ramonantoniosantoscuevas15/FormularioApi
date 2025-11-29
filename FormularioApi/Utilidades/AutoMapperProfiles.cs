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
            CreateMap<Persona, PersonaDTO>()
                //obteniendo una persona con sus categorias
                .ForMember(p => p.Categorias, entidad => entidad.MapFrom(c => c.CategoriaPersonas.Select(cp =>
                new CategoriaDTO { Id = cp.CategoriaId, Tipo = cp.Categoria.Tipo })));
            //mapeando los correos
            CreateMap<CrearCorreoDTO, Correo>();
            CreateMap<Correo, CorreoDTO>();
            //mapeando las dirreciones
            CreateMap<CrearDirrecionDTO, Dirrecion>();
            CreateMap<Dirrecion, DirrecionDTO>();
            //mapeando los telefonos
            CreateMap<CrearTelefonoDTO, Telefono>();
            CreateMap<Telefono, TelefonoDTO>();
            //mapeando las categorias
            CreateMap<CrearCategoriaDTO, Categoria>();
            CreateMap<Categoria, CategoriaDTO>();
        }
    }
}
