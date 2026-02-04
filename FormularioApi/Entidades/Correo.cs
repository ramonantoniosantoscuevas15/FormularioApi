using System.ComponentModel.DataAnnotations;

namespace FormularioApi.Entidades
{
    public class Correo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string correos { get; set; }

        public int PersonaId { get; set; }
    }
}
