namespace FormularioApi.DTOs
{
    public class CrearPersonaDTO
    {
        public required string nombre { get; set; } = null!;
        public required string apellido { get; set; } = null!;
        public DateTime? fechanacimiento { get; set; }
        public int? cedula { get; set; }
        public List<CrearCorreoDTO> Correos { get; set; } = null!;
        public List<CrearDirreccionDTO> Dirrecciones { get; set; } = null!;
        public List<CrearTelefonoDTO> Telefonos { get; set; } = null!;
    }
}
