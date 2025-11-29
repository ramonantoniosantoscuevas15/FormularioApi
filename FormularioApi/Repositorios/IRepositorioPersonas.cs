using FormularioApi.DTOs;
using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task Actualizar(Persona personas);
        Task Asignarcategoria(int id, List<int> categoriasIds);
        Task Borrar(int id);
        Task<List<Persona>> BusquedaPorNombre(string nombre);
        Task<int> Crear(Persona personas);
        Task<bool> Existe(int id);
        Task<Persona?> ObtenerPorId(int id);
        Task<List<Persona>> ObtenerTodos(PaginacionDTO paginacionDTO);
    }
}