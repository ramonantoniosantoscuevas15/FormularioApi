using FormularioApi.Entidades;

namespace FormularioApi.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public List<CategoriaPersona> CategoriaPersonas { get; set; } = new List<CategoriaPersona>();
    }
}
