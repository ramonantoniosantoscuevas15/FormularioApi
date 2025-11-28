namespace FormularioApi.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public List<CategoriaPersona> CategoriaPersonas { get; set; } = new List<CategoriaPersona>();

    }
}
