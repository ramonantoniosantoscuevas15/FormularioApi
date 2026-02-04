using System.ComponentModel.DataAnnotations;

namespace FormularioApi.Entidades
{
    public class Dirreccion
    {
        public int Id { get; set; }
        public string? tipodirrecion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string ubicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string ciudad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string provincia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string codigopostal { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string pais { get; set; }

        public int PersonaId { get; set; }

    }
}
