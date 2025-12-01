using FormularioApi.Entidades;

namespace FormularioApi.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public List<CategoriaDTO> Categorias { get; set; } = new List<CategoriaDTO>();
        public List<Correo> Correos { get; set; } = new List<Correo>();
        public List<Dirrecion> Dirreciones { get; set; } = new List<Dirrecion>();
        public List<Telefono> Telefonos { get; set; } = new List<Telefono>();
       
    }
}
