namespace FormularioApi.Entidades
{
    public class Dirrecion
    {
       public int Id { get; set; }
       public string Tipo { get; set; } = null!;
       public string Ubicacion { get; set; } = null!;
       public string Ciudad {  get; set; } = null!;
       public string Provicia { get; set; } = null!;
       public string CodigoPostal { get; set; } = null!;
       public string Pais { get; set; } = null!;
       public int PersonaId { get; set; }

    }
}
