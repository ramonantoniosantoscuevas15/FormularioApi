using System.ComponentModel.DataAnnotations;

namespace FormularioApi.Entidades
{
    public class Telefono
    {
        public int Id { get; set; }
        public string? tiponumero { get; set; }

        public string? codigopais { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(7, 20, ErrorMessage = "El campo {0} debe estar entre {1} y {2} digitos")]

        public required int numero { get; set; }

        public int PersonaId { get; set; }
    }
}
