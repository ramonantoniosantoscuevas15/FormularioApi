namespace FormularioApi.DTOs
{
    public class CrearTelefonoDTO
    {

        public string? tiponumero { get; set; }
        public string? codigopais { get; set; }
        public required int numero { get; set; }

    }
}
