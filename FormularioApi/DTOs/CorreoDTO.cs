namespace FormularioApi.DTOs
{
    public class CorreoDTO
    {
        public int Id { get; set; }
        public string Correos { get; set; } = null!;
        public int PersonaId { get; set; }
    }
}
