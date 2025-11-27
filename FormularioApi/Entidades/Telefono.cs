namespace FormularioApi.Entidades
{
    public class Telefono
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string CodigoPais { get; set; } = null!;
        public int Numero {  get; set; }
        public int PersonaId { get; set; }
    }
}
