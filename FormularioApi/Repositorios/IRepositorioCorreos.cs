using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioCorreos
    {
        Task Actualizar(Correo correos);
        Task Borrar(int id);
        Task<int> Crear(Correo correos);
        Task<bool> Existe(int id);
        Task<Correo?> ObtenerPorId(int id);
        Task<List<Correo>> ObtenerTodos(int personaId);
    }
}