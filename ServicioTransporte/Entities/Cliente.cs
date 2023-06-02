using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trCliente")]
    public class Cliente
    {
        public int Id { get; set; }
        public int Id_Estado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public Estado Estado { get; set; }
    }
}
