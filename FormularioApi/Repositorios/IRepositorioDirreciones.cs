using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioDirreciones
    {
        Task Actualizar(Dirrecion dirrecion);
        Task Borrar(int id);
        Task<int> Crear(Dirrecion dirreciones);
        Task<bool> Existe(int id);
        Task<Dirrecion?> ObtenerPorId(int id);
        Task<List<Dirrecion>> ObtenerTodos(int personaId);
    }
}