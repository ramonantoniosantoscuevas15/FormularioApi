namespace FormularioApi.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string tipo { get; set; } = null!;
        public List<CategoriaPersona> CategoriaPersonas { get; set; } = new List<CategoriaPersona>();

    }
}
