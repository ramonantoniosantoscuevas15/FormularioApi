using AutoMapper;
using FormularioApi.DTOs;
using FormularioApi.Entidades;

namespace FormularioApi.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            
            ConfiguracionCategoria();
            ConfiguracionPersonas();

        }
        private void ConfiguracionCategoria()
        {
            //mapeando las categorias
            CreateMap<CrearCategoriaDTO, Categoria>();
            CreateMap<Categoria, CategoriaDTO>();

        }
        private void ConfiguracionPersonas()
        {
            //mapeando las personas
            CreateMap<CrearPersonaDTO, Persona>()
                .ForMember(entidad => entidad.Correos, dto => dto.MapFrom(dto => dto.Correos))
                .ForMember(entidad => entidad.Dirrecciones, dto => dto.MapFrom(dto => dto.Dirrecciones))
                .ForMember(entidad => entidad.Telefonos, dto => dto.MapFrom(dto => dto.Telefonos));
            CreateMap<CrearCorreoDTO, Correo>();
            CreateMap<CrearDirreccionDTO, Dirreccion>();
            CreateMap<CrearTelefonoDTO, Telefono>();

            CreateMap<Persona, PersonaDTO>()
                //obteniendo una persona con sus categorias
                .ForMember(p => p.Categorias, entidad => entidad.MapFrom(c => c.CategoriaPersonas.Select(cp =>
                new CategoriaDTO { Id = cp.CategoriaId, Tipo = cp.Categoria.tipo })));
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<Persona, PersonaDTO>();
            //mapeando los correos

            CreateMap<Correo, CorreoDTO>();
            //mapeando las dirreciones
            
            CreateMap<Dirreccion, DirreccionDTO>();
            //mapeando los telefonos
            
            CreateMap<Telefono, TelefonoDTO>();

        }

    }
}
