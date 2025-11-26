namespace FormularioApi.Entidades
{
    public class Correo
    {
        public int Id { get; set; }
        public string Correos { get; set; } = null!;
        public int PersonaId { get; set; }
    }
}
