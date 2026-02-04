using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioDirreciones
    {
        Task Actualizar(Dirreccion dirrecion);
        Task Borrar(int id);
        Task<int> Crear(Dirreccion dirreciones);
        Task<bool> Existe(int id);
        Task<Dirreccion?> ObtenerPorId(int id);
        Task<List<Dirreccion>> ObtenerTodos(int personaId);
    }
}