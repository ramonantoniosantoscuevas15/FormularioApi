using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioTelefonos
    {
        Task Actualizar(Telefono telefonos);
        Task Borrar(int id);
        Task<int> Crear(Telefono telefonos);
        Task<bool> Existe(int id);
        Task<Telefono?> ObtenerPorId(int id);
        Task<List<Telefono>> ObtenerTodos(int personaId);
    }
}