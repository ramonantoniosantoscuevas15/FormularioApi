namespace FormularioApi.Entidades
{
    public class CategoriaPersona
    {
        public int PersonaId { get; set; }
        public int CategoriaId { get; set; }
        public Persona Persona { get; set; } = null!;
        public Categoria Categoria { get; set; } = null!;
    }
}
