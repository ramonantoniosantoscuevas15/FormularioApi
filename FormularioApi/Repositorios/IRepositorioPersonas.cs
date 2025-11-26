using FormularioApi.Entidades;

namespace FormularioApi.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task Actualizar(Persona personas);
        Task Borrar(int id);
        Task<List<Persona>> BusquedaPorNombre(string nombre);
        Task<int> Crear(Persona personas);
        Task<bool> Existe(int id);
        Task<Persona?> ObtenerPorId(int id);
    }
}