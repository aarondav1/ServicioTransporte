using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioTransporte.Entities
{
    [Table("trParada")]
    public class Parada
    {
        public int Id { get; set; }
        public int Id_Estado { get; set; }
        public string Direccion { get; set; }
        public List<AsignacionRutaParada> AsignacionesRutaParada { get; set; }
    }
}
