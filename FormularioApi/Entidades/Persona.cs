namespace FormularioApi.Entidades
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public List<Correo> Correos  { get; set; } = new List<Correo>();
    }
}
