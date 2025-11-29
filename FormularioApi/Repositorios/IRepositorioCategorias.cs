using FormularioApi.DTOs;
using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categoria categorias);
        Task Borrar(int id);
        Task<int> Crear(Categoria categorias);
        Task<bool> Existe(int id);
        Task<List<int>> Existen(List<int> ids);
        Task<Categoria?> ObtenerPorId(int id);
        Task<List<Categoria>> ObtenerTodos(PaginacionDTO paginacionDTO);
    }
}